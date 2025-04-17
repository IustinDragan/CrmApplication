using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Repositories.Interfaces;
using CRMRealEstate.DataAccess.Scripts;
using Microsoft.EntityFrameworkCore;

namespace CRMRealEstate.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DatabaseContext _databaseContext;

    public UsersRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Users> AddAsync(Users user)
    {
        var addedEntity = await _databaseContext.Users.AddAsync(user);

        await _databaseContext.SaveChangesAsync();

        return addedEntity.Entity;
    }

    public async Task<Users> UpdateAsync(int id)
    {
        var updatedEntity = await _databaseContext.Users.Where(x => x.Id == id).Include(x => x.Company).FirstAsync();

        await _databaseContext.SaveChangesAsync();

        return updatedEntity;
    }

    public async Task<List<Users>> ReadAllAsync()
    {
        return await _databaseContext.Users.Include(c => c.Company).ToListAsync();
    }

    public async Task<Users?> GetById(int id)
    {
        return await _databaseContext.Users.Include(c => c.Company).Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> GetEmail(string email)
    {
        return await _databaseContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task DeleteAsync(int id)
    {
        var userToDelete = await GetById(id);

        if (userToDelete == null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        _databaseContext.Users.Remove(userToDelete);

        await _databaseContext.SaveChangesAsync();
    }

    public async Task<Users?> GetByUsername(string username)
    {
        return await _databaseContext.Users.Include(c => c.Company)
            .Where(x => x.UserName == username)
            .FirstOrDefaultAsync();
    }

    public async Task<Users?> GetUserWithFavoritesAsync(int userId)
    {
        return await _databaseContext.Users
        .Include(u => u.FavoritesAnnouncements)
            .ThenInclude(fav => fav.Announcement)
        .Include(u => u.Company)
        .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task AddFavoriteAnnouncementsAsync(UserAnnouncement userAnnouncement)
    {
        await _databaseContext.Set<UserAnnouncement>().AddAsync(userAnnouncement);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<List<UserAnnouncement>> GetFavoriteAnnouncementsAsync(int userId)
    {
        return await _databaseContext.UsersAnnouncements
            .Where(ua => ua.UserId == userId)
            .Include(ua => ua.Announcement)
                .ThenInclude(prop => prop.Property)
            .ToListAsync();
    }

    public Task<UserAnnouncement?> GetFavoriteAnnouncementAsync(int userId, int announcementId)
    {
        throw new NotImplementedException();
    }

    //public async Task<List<Announcement>> GetFavoriteAnnouncementsAsync(int userId)
    //{
    //    return await _databaseContext.UsersAnnouncements
    //        .Where(ua => ua.UserId == userId)
    //        .Include(ua => ua.Announcement)
    //        .Select(ua => ua.Announcement)
    //        .ToListAsync();
    //}
}
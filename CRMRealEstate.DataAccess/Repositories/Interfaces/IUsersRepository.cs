﻿using CRMRealEstate.DataAccess.Entities;

namespace CRMRealEstate.DataAccess.Repositories.Interfaces;

public interface IUsersRepository
{
    Task<Users> AddAsync(Users user);

    Task<Users> UpdateAsync(int id);

    Task<List<Users>> ReadAllAsync();

    Task<Users?> GetById(int id);

    Task<bool> GetEmail(string email);

    Task DeleteAsync(int id);

    Task<Users?> GetByUsername(string username);

    Task<Users?> GetUserWithFavoritesAsync(int userId);

    Task AddFavoriteAnnouncementsAsync(UserAnnouncement userAnnouncement);

    Task<List<UserAnnouncement>> GetFavoriteAnnouncementsAsync(int userId);
}
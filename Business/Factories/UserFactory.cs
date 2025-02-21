﻿using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class UserFactory
{
    public static UserRegistrationForm CreateRegistrationForm() => new();
    public static UserUpdateForm CreateUpdateForm() => new();


    public static UserEntity? Create(UserRegistrationForm form) => form == null ? null : new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email
    };

    public static User? Create(UserEntity entity) => entity == null ? null : new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Email = entity.Email
    };

    public static UserUpdateForm Create(User user) => new()
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email
    };
    public static UserEntity Create(UserEntity userEntity, UserUpdateForm form) => new()
    {
        Id = userEntity.Id,
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email
    };
}

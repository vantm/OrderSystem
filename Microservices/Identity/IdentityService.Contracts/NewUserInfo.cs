﻿namespace IdentityService.Contracts;

public record NewUserInfo(
    string UserName,
    string Password,
    string FullName,
    string EmailAddress,
    bool IsActive)
{
}
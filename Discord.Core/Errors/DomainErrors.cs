﻿using Discord.Core.Shared;

namespace Discord.Core.Errors;

public class DomainErrors
{
    public class Email
    {
        public static readonly Error Empty = new(
            "Email.Empty",
            "Email is empty.");

        public static readonly Error InvalidFormat = new(
            "Email.InvalidFormat",
            "Email format is invalid.");
    }
    
    public class Password
    {
        public static readonly Error Empty = new(
            "Password.Empty",
            "Password is empty.");
        
        public static readonly Error InvalidPassword = new(
            "Password.Invalid",
            "Password must contain min. 6 ch.");
    }
    
    public class User
    {
        public static readonly Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "The provided credentials are invalid.");
        
        public static readonly Error EmailAlreadyExist = new(
            "User.EmailAlreadyExist",
            "User with this email already exist");
        
        public static readonly Error UserNotFound = new(
            "User.UserNotFound",
            "User not found");
    }
    
    
    public class Birthday
    {
        public static readonly Error InvalidYear = new(
            "Birthday.InvalidYear",
            "The provided year value are invalid.");
        
        public static readonly Error InvalidMonth = new(
            "Birthday.InvalidMonth",
            "The provided month value are invalid.");
        
        public static readonly Error InvalidDay = new(
            "Birthday.InvalidDay",
            "The provided day value are invalid.");
    }
    
    public class FriendRequest
    {
        public static readonly Error RequestAlreadyExist = new(
            "FriendRequest.RequestAlreadyExist",
            "Request already exist.");
        
        public static readonly Error UsersAlreadyFriends = new(
            "FriendRequest.UsersIsAlreadyFriends",
            "Users already friends.");
    }
}
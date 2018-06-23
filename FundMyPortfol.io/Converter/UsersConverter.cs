using FundMyPortfol.io.Models;
using FundMyPortfol.io.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundMyPortfol.io.Converter
{
    public class UsersConverter
    {
        
        public UserView UsertoUserViewConverter(User user)
        {
            UserView userView = new UserView
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                ProjectCounter = user.ProjectCounter,
                Followers = user.Followers,
                FirstName = user.UserDetailsNavigation?.FirstName,
                LastName = user.UserDetailsNavigation?.LastName,
                Country = user.UserDetailsNavigation?.Country,
                Town = user.UserDetailsNavigation?.Town,
                Street = user.UserDetailsNavigation?.Street,
                PostalCode = user.UserDetailsNavigation?.PostalCode,
                PhoneNumber = user.UserDetailsNavigation?.PhoneNumber,
                ProfileImage = user.UserDetailsNavigation?.ProfileImage,
                Project = user.Project
            };
            return userView;
        }

        public User UserViewtoUserConverter(UserView user)
        {
            User userModel = new User
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password
            };
            return userModel;
        }

        public UserDetails UserViewtoUserDetailsConverter(UserView user)
        {
            UserDetails userDetails = new UserDetails
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country,
                Town = user.Town,
                Street = user.Street,
                PostalCode = user.PostalCode,
                PhoneNumber = user.PhoneNumber,
                ProfileImage = user.ProfileImage
            };
            return userDetails;
        }
    }
}

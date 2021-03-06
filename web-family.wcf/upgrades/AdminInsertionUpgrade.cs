﻿using web_family.core.datacontracts;
using web_family.wcf.interfaces;

namespace web_family.wcf.upgrades
{
    public class AdminInsertionUpgrade : IUpgrade
    {
        private readonly IUserRepository _userRepository;

        public AdminInsertionUpgrade(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public void Upgrade()
        {
            if (_userRepository.ExistsAsyn("superuser").Result) return;

            var admin = new User
            {
                Fullname = "superuser",
                Id = "superuser",
                Username = "superuser",
                Password = "superuser"
            };
            _userRepository.InsertOneAsync(admin);
        }

    }
}
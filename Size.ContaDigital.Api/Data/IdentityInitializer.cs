﻿using System;
using Microsoft.AspNetCore.Identity;
using Size.ContaDigital.Api.Models;

namespace Size.ContaDigital.Api.Data
{
    public class IdentityInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public IdentityInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_context.Database.EnsureCreated())
            {
                if (!_roleManager.RoleExistsAsync(Roles.ROLE_API_CONTADIGITAL).Result)
                {
                    var resultado = _roleManager.CreateAsync(
                        new IdentityRole(Roles.ROLE_API_CONTADIGITAL)).Result;
                    if (!resultado.Succeeded)
                    {
                        throw new Exception(
                            $"Erro durante a criação da role {Roles.ROLE_API_CONTADIGITAL}.");
                    }
                }

                CreateUser(
                    new ApplicationUser()
                    {
                        UserName = "admin_apiacontadigital",
                        Email = "admin-apicontadigital@teste.com.br",
                        EmailConfirmed = true
                    }, "AdminAPICONTADIGITAL01!", Roles.ROLE_API_CONTADIGITAL);

                CreateUser(
                    new ApplicationUser()
                    {
                        UserName = "usrinvalido_apicontadigital",
                        Email = "usrinvalido-apicontadigital@teste.com.br",
                        EmailConfirmed = true
                    }, "UsrInvAPICONTADIGITAL01!");
            }
        }

        private void CreateUser(
            ApplicationUser user,
            string password,
            string initialRole = null)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var resultado = _userManager
                    .CreateAsync(user, password).Result;

                if (resultado.Succeeded &&
                    !String.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }
        }
    }
}
﻿namespace FastFood.Core.Controllers
{
    using System;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using FastFood.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ViewModels.Employees;

    public class EmployeesController : Controller
    {
        private readonly FastFoodContext _context;
        private readonly IMapper _mapper;

        public EmployeesController(FastFoodContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Register()
        {

            var position = await _context.Positions
                .ProjectTo<RegisterEmployeeViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
           return View(position);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterEmployeeInputModel model)
        {
            if(!ModelState.IsValid)
            {
                RedirectToAction("Error", "Home");
            }
            var employee = _mapper.Map<Employee>(model);
           await _context.Employees.AddAsync(employee);
           await _context.SaveChangesAsync();
            return RedirectToAction("All");


        }

        public async Task<IActionResult> All()
        {
            var empoyees = await _context.Employees
                .Select(e=> new EmployeesAllViewModel
                {
                    Name=e.Name,
                    Age=e.Age,
                    Address=e.Address,
                    Position=e.Position.Name

                })
                .ToListAsync();


            return View(empoyees);
        }
    }
}

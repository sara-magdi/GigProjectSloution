using DAL;
using GigHubProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GigHubProject.ViewModels
{
    public class GigformViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public string Action {
            get 
            {
                Expression < Func < GigsController,Task < IActionResult >>> Update =
                    (e => e.Update(this));
                Expression<Func<GigsController, Task<IActionResult>>> Create =
                    (e => e.Create(this));
                var action = (Id != 0) ? Update : Create;
                var ActionName = (Id != 0) ? "Update" : "Create";
                // return (action.Body as MethodCallExpression).Method.Name;
                return ActionName;
            } 
        }

        public string Heading { get; set; }
        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0},{1}",Date,Time));
        }
    }
}

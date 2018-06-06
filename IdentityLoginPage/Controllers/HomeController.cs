using System;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity;
using IdentityLoginPage.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IdentityLoginPage.Controllers
{
	
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
		[Authorize]
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}
		[Authorize]
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		[Authorize]
		public ActionResult Menu()
		{
			CoffeeEntities db = new CoffeeEntities();
			List<Item> items = db.item.ToList();
			ViewBag.Items = items;

			ViewBag.Statuses = db.item.ToList();

			return View();
		}
		//filter by Name
		[Authorize]
		public ActionResult MenuByName(string name)
		{
			CoffeeEntities db = new CoffeeEntities();
			//LINQ Query
			List<Item> items = (from i in db.item
								where i.Name == name
								select i).ToList();
			ViewBag.Name = items;

			ViewBag.Name = db.item.ToList();

			return View("Menu");
		}
		[Authorize]
		public ActionResult MenuByDescription(string description)
		{
			CoffeeEntities db = new CoffeeEntities();
			//LINQ Query
			List<Item> items = (from i in db.item
								where i.Description.Contains(description)
								select i).ToList();
			ViewBag.Items = items;

			ViewBag.Names = db.item.ToList();

			return View("Menu");
		}
		[Authorize]
		public ActionResult MenuSorted(string column)
		{
			CoffeeEntities db = new CoffeeEntities();
			//LINQ Query
			if (column == "ID")
			{
				ViewBag.Items = (from i in db.item
								 orderby i.ID
								 select i).ToList();
			}
			else if (column == "Name")
			{
				ViewBag.Items = (from i in db.item
								 orderby i.Name
								 select i).ToList();
			}
			else if (column == "Description")
			{
				ViewBag.Items = (from i in db.item
								 orderby i.Description
								 select i).ToList();
			}
			else if (column == "Quantity")
			{
				ViewBag.Items = (from i in db.item
								 orderby i.Quantity
								 select i).ToList();
			}
			else if (column == "Price")
			{
				ViewBag.Items = (from i in db.item
								 orderby i.Price
								 select i).ToList();
			}

			ViewBag.Statuses = db.item.ToList();

			return View("Menu");
		}

		public ActionResult Add(int id)
		{
			CoffeeEntities db = new CoffeeEntities();

			//check if the Cart object already exists
			if (Session["Cart"] == null)
			{
				List<Item> cart = new List<Item>();
				cart.Add((from i in db.item
						  where i.ID == id
						  select i).Single());

				Session.Add("Cart", cart);
			}
			else
			{
				//if it does exist, get the list
				List<Item> cart = (List<Item>)(Session["Cart"]);
				//add this book to it
				cart.Add((from i in db.item
						  where i.ID == id
						  select i).Single());
				//(add it back to the session)
				Session["Cart"] = cart;
			}
			return View();
		}
	}
}
	

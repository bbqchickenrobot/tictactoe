﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject.Modules;
using TicTacToe.Common.Interfaces;
using TicTacToe.Common.Repositories;
using TicTacToe.WebUI.Decorators;
using TicTacToe.WebUI.Managers;
using TicTacToe.WebUI.Models;

namespace TicTacToe.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IPlayerRepository _playerRepository;
        private IDiscColorManager _discColorManager;
        
        public HomeController(IPlayerRepository playerRepository, IDiscColorManager discColorManager)
        {
            _playerRepository = playerRepository;
            _discColorManager = discColorManager;
        }

        public ActionResult Index()
        {
            var players = _playerRepository.GetPlayers();

            var model = new IndexModel
                {
                    Players = players.Select(p => new ColoredPlayer(p) { RgbColor = _discColorManager.GetDiscColor(p.Name[0]) })
                };

            ViewBag.Message = "Spela spel!";
            ViewBag.Title = "Spela";
            ViewBag.PlayersTitle = "Spelare";

            return View(model);
        }

        public ActionResult ClearColors()
        {
            _discColorManager.ClearDiscColors();

            return RedirectToAction("Index");
        }
    }

}

﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using TicTacToe.Entities;
using TicTacToe.Entities.Repositories;
using TicTacToe.WebUI.Controllers;
using TicTacToe.WebUI.Managers;
using TicTacToe.WebUI.Models;

namespace TicTacToe.WebUI.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        public Mock<IPlayer> Player1
        {
            get
            {
                var player1 = new Mock<IPlayer>();
                player1.SetupGet(p => p.Name).Returns("Tobias");

                return player1;
            }
        }

        public Mock<IPlayer> Player2
        {
            get
            {
                var player2 = new Mock<IPlayer>();
                player2.SetupGet(p => p.Name).Returns("Unknown");

                return player2;
            }
        }

        private Mock<IPlayerRepository> PlayerRepo
        {
            get
            {
                var repo = new Mock<IPlayerRepository>();
                repo.Setup(r => r.GetPlayers()).Returns(new List<IPlayer> { Player1.Object, Player2.Object });

                return repo;
            }
        }

        private Mock<IDiscColorManager> DiscColorManager
        {
            get
            {
                var manager = new Mock<IDiscColorManager>();
                manager.Setup(m => m.GetDiscColor('T')).Returns("rgb(0,0,0)");
                return manager;
            }
        }

        [Test]
        public void ShouldClearColors()
        {
            var manager = DiscColorManager;

            var controller = new HomeController(PlayerRepo.Object, manager.Object);

            controller.ClearColors();

            manager.Verify(m => m.ClearDiscColors());
        }
        
        [Test]
        public void ShouldRedirectToIndex()
        {

            PlayerRepo.Setup(r => r.GetPlayers()).Returns(new List<IPlayer> { Player1.Object, Player2.Object });

            DiscColorManager.Setup(m => m.GetDiscColor('T')).Returns("rgb(0,0,0)");

            var controller = new HomeController(PlayerRepo.Object, DiscColorManager.Object);

            var result = controller.ClearColors();

            Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
        }

        [Test]
        public void IndexShouldContainModel()
        {
            var controller = new HomeController(PlayerRepo.Object, DiscColorManager.Object);

            var result = controller.Index() as ViewResult;

            Assert.IsTrue(result.Model != null);
        }

        [Test]
        public void IndexModelShouldContainTwoPlayers()
        {
            PlayerRepo.Setup(p => p.GetPlayers()).Returns(new List<IPlayer>() {Player1.Object, Player2.Object});

            var controller = new HomeController(PlayerRepo.Object, DiscColorManager.Object);

            var result = controller.Index() as ViewResult;
            var model = result.Model as IndexModel;
            var players = new List<IPlayer>(model.Players);

            Assert.IsTrue(players.Count == 2);
        }
    }
}

﻿using MemoryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace MemoryName.Controllers
{
    public class GameController : ApiController
    {
        [Route("api/getGame/{userName}")]
        [HttpGet]
        public IHttpActionResult GetGame(string userName)
        {
            var currentGame = Global.GameList.FirstOrDefault(g => g.Player1.UserName == userName || g.Player2.UserName == userName);
            if (currentGame == null)
                return BadRequest();
            return Ok(new { currentGame.CardArray, currentGame.CurrentTurn });
        }

        [Route("api/updateTurn/{userName}/{firstCard}/{secondCard}")]
        [HttpPut]
        public IHttpActionResult UpdateTurn(string userName, string firstCard, string secondCard)
        {

            var currentGame = Global.GameList.FirstOrDefault(g => g.CurrentTurn == userName);
            if (currentGame == null)
                return BadRequest();
            lock (currentGame)
            {


                if (firstCard == secondCard)
                {
                    currentGame.CardArray[firstCard] = userName;
                    if (!currentGame.CardArray.Any(c => c.Value == null))
                    {
                       EndGame(currentGame);
                    }
                }
                currentGame.CurrentTurn = currentGame.CurrentTurn == currentGame.Player1.UserName ? currentGame.Player2.UserName : currentGame.Player1.UserName;

            }
            return Ok();
        }
      
        private void EndGame(Game currentGame)
        {

            var p1Cards = currentGame.CardArray.Count(c => c.Value == currentGame.Player1.UserName);
            var p2Cards = currentGame.CardArray.Count(c => c.Value == currentGame.Player2.UserName);
            currentGame.Player1.PartnerName = null;
            currentGame.Player2.PartnerName = null;
            if (p1Cards > p2Cards)
            {
                currentGame.Player1.Score++;
            }

            else
            {
                currentGame.Player2.Score++;
            }
            Task t = new Task(()=> {
                Thread.Sleep(3000);
                Global.GameList.Remove(currentGame);
            });
            t.Start();
         
        }
    }
}

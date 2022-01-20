using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using ConsoleGameDev.Entities;
using ConsoleGameDev.Entities.Abstract;
using ConsoleGameDev.Enums;
using ConsoleGameDev.Render;
using ConsoleGameDev.Render.Interfaces;

namespace ConsoleGameDev
{
    public class GameOrchestrator
    {
        private Thread playerMoveThread;
        private Thread enemyMoveThread;
        private Thread bulletThread;
        private Thread bonusesThread;
        private Thread infoThread;
        private Thread dispatcherThread;
        private IField gameField;
        
        private CollectableEntity lastSpawnedBonus;
        private ObservableCollection<Entity> _entities;


        private readonly Player _player;
        private TimeSpan BonusesSpawnRatio=TimeSpan.FromSeconds(10);
        private TimeSpan DisplayInfoRatio=>TimeSpan.FromSeconds(0.1);

        private bool GameStopped = false;

        private void StopGame() => GameStopped = true;

        private (int, int) PlayerPosition => gameField.PositionOf(_player.Key);
        public GameOrchestrator(int fieldSize)
        {
            gameField = new Field(fieldSize,new ConsoleRenderer());
            _entities = new ObservableCollection<Entity>();
            _player = new Player();
            _entities.Add(_player);
            gameField.PlaceObject(_player,0,0);
            gameField.PlaceObject(new Obstacle(),3,3);
            gameField.PlaceObject(new Obstacle(),10,5);
            gameField.PlaceObject(new Obstacle(),1,16);
            playerMoveThread = new Thread(ListenUserInput);
            playerMoveThread.Start();
            lock(gameField)
            {
                

                bonusesThread = new Thread(SpawnBonuses<Ammo>);
                bonusesThread.Start();
                enemyMoveThread = new Thread(() =>
                {
                    var bob = new Invader(_player);
                    _entities.Add(bob);
                    gameField.PlaceObject(bob, gameField.Size / 2, gameField.Size - bob.GetPoints().GetLength(1));
                    bob.DoLifeCycle(gameField);
                });
                enemyMoveThread.Start();

                infoThread = new Thread(DisplayInfo);
                infoThread.Start();
            }
        }
        

        private void DisplayInfo()
        {
            while (!GameStopped)
            {
                gameField.DrawTextInformation(_player.GetStatistics());
                Thread.Sleep(DisplayInfoRatio);
            }
        }
        

        private void SpawnBonuses<T>() where T : CollectableEntity
        {
            var random = new Random();
            while (!GameStopped)
            {
                var (x, y) = (random.Next(0, gameField.Size), random.Next(0, gameField.Size));
                var bonus = new Ammo(_player);
                if(lastSpawnedBonus!=null)
                    try
                    {
                        gameField.RemoveObject(lastSpawnedBonus);
                    }
                    catch
                    {
                        // ignored
                    }
                gameField.PlaceObject(bonus,x,y);
                lastSpawnedBonus = bonus;
                Thread.Sleep(BonusesSpawnRatio);
            }
        }

        private void ListenUserInput()
        {
            while (!GameStopped)
            {
                var cki = Console.ReadKey(true);
                if (_player.Locked) return;
                switch (cki.Key)
                {
                    case ConsoleKey.W:
                    {
                        gameField.MoveObject(_player,PlayerPosition.Item1,PlayerPosition.Item2+1);
                        _player.CheckAndCollect<Ammo>(gameField,MovingDirection.Up);
                        break;
                    }
                    case ConsoleKey.A:
                    {
                        gameField.MoveObject(_player,PlayerPosition.Item1-1,PlayerPosition.Item2);
                        _player.CheckAndCollect<Ammo>(gameField,MovingDirection.Left);
                        break;
                    }
                    case ConsoleKey.S:
                    {
                        gameField.MoveObject(_player,PlayerPosition.Item1,PlayerPosition.Item2-1);
                        _player.CheckAndCollect<Ammo>(gameField,MovingDirection.Down);
                        break;
                    }
                    case ConsoleKey.D:
                    {
                        gameField.MoveObject(_player,PlayerPosition.Item1+1,PlayerPosition.Item2);
                        _player.CheckAndCollect<Ammo>(gameField,MovingDirection.Right);
                        break;
                    }
                    case ConsoleKey.G:
                    {
                        lock(gameField)
                        {
                            var bullet = _player.Shoot();
                            if (bullet == null) break;
                            gameField.PlaceObject(bullet, PlayerPosition.Item1 + 1, PlayerPosition.Item2 + 3);
                            bulletThread = new Thread(() => bullet.DoLifeCycle(gameField));
                            bulletThread.Start();
                        }
                        break;
                    }
                    case ConsoleKey.Q:
                    {
                        gameField.DrawSnapshotDebug();
                        break;
                    }
                }
            }
        }
        
    }
}
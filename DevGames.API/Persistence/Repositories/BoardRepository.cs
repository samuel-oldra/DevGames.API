﻿using DevGames.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevGames.API.Persistence.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly DevGamesContext context;

        public BoardRepository(DevGamesContext context)
            => this.context = context;

        public IEnumerable<Board> GetAll()
            => context.Boards.ToList();

        public Board? GetById(int id)
            => context.Boards.Include(b => b.Posts).SingleOrDefault(b => b.Id == id);

        public void Add(Board board)
        {
            context.Boards.Add(board);
            context.SaveChanges();
        }

        public void Update(Board board)
        {
            context.Boards.Update(board);
            context.SaveChanges();
        }

        public bool BoardExists(int id)
            => context.Boards.Any(b => b.Id == id);
    }
}
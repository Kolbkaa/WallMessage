using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WallMessage.Data;
using WallMessage.Models;

namespace WallMessage.Services
{
    public class MessageManager
    {
        private readonly AppDbContext _appDbContext;

        public MessageManager(AppDbContext appAppDbContext)
        {
            _appDbContext = appAppDbContext;
        }

        public async Task Add(Message model)
        {
            await _appDbContext.Messages.AddAsync(model);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Message>> GetAll()
        {
            if (await _appDbContext.Messages.AnyAsync() == false)
                return null;

            return await _appDbContext.Messages.Include(message => message.User).ToListAsync();
        }

        public async Task<Message> Get(int id, User user)
        {
            return await _appDbContext.Messages.SingleOrDefaultAsync(message =>
                message.Id == id && message.User.Equals(user));
        }

        public async Task Edit(Message message)
        {
            _appDbContext.Messages.Update(message);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id, User user)
        {
            var messageToDelete = await _appDbContext.Messages.SingleOrDefaultAsync(message => message.Id == id && message.User.Equals(user));

            _appDbContext.Messages.Remove(messageToDelete);

            await _appDbContext.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestChat.dal;
using TestChat.Model;
using log4net;
using TestChat.DataView;

namespace TestChat.Business
{
    public interface IChatPostService
    {
        IList<ChatPost> GetAll();
        void Add(ChatPost obj);
    }
    public class ChatPostService : IChatPostService
    {
        ILog logger = LogManager.GetLogger(typeof(ChatPostService));
        IChatPostRepository repository;
        public ChatPostService()
        {
            repository = new ChatPostRepository();
        }
        public IList<ChatPost> GetAll()
        {
            return repository.GetAll();
        }

        public void Add(ChatPost obj)
        {
            repository.Add(obj);
        }

        public void SavePost(string connectionID, string message)
        {
            logger.Info("saving post.");
            try
            {
                UserService userService = new UserService();
                var user = userService.GetByConnectionID(connectionID);

                if (user != null)
                {
                    ChatPost chatPost = new ChatPost();
                    chatPost.UserID = user.UserID;
                    chatPost.ChatPosting = message.Replace("'", "''");
                    chatPost.PostedAt = DateTime.Now;

                    Add(chatPost);
                }
                else
                {
                    logger.Error("User null from Save Post.");
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
        public IList<RecentPostView> GetRecentPosts()
        {
            var chatPost = repository.GetAll().OrderByDescending(r => r.PostedAt).Take(15).ToList<ChatPost>();
            var users = new UserService().GetAll();

            var q = from c in chatPost
                    join u in users on c.UserID equals u.UserID
                    select new RecentPostView {
                        UserName = u.UserName,
                        Message = c.ChatPosting
                    };

            return q.ToList();
        }

    }
}

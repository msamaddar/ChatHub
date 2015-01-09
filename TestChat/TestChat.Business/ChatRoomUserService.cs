using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestChat.dal;
using TestChat.DataView;
using TestChat.Model;

namespace TestChat.Business
{
    public interface IChatRoomUserService
    {
        IList<ChatRoomUser> GetByRoomName(string roomName);
        ChatRoomUser GetByUserName(string userName);
        void Add(ChatRoomUser obj);
        void Remove(ChatRoomUser obj);
    }
    public class ChatRoomUserService : IChatRoomUserService
    {
        IChatRoomUserRepository repository;
        public ChatRoomUserService()
        {
            repository = new ChatRoomUserRepository();
        }
        public IList<ChatRoomUser> GetByRoomName(string roomName)
        {
            return repository.GetList(r => r.ChatRoom.ChatRoomName.Equals(roomName), r => r.ChatRoom);
        }

        public ChatRoomUser GetByUserName(string userName)
        {
            return repository.GetSingle(r => r.User.UserName.Equals(userName), r => r.User);
        }
        public void Add(ChatRoomUser obj)
        {
            repository.Add(obj);
        }

        public void Remove(ChatRoomUser obj)
        {
            repository.Remove(obj);
        }

        public void AddUserToChatRoom(string userName, string roomName)
        {
            var chatRoomUser = GetByUserName(userName);

            if (chatRoomUser == null)
            {
                ChatRoomService chatRoomService = new ChatRoomService();
                var chatRoom = chatRoomService.GetByRoomName(roomName);

                UserService userService = new UserService();
                var user = userService.GetByUserName(userName);

                if (chatRoom != null && user != null)
                {
                    chatRoomUser = new ChatRoomUser();
                    chatRoomUser.UserID = user.UserID;
                    chatRoomUser.ChatRoomID = chatRoom.ChatRoomID;
                    Add(chatRoomUser);
                }
            }
        }

        public void RemoveUserFromChatRoom(string connectionID, string roomName)
        {
            UserService userService = new UserService();
            var user = userService.GetByConnectionID(connectionID);

            if (user != null)
            {
                var chatRoomUser = GetByUserName(user.UserName);

                if (chatRoomUser != null)
                {
                    Remove(chatRoomUser);
                }
            }
        }
        public List<UserView> GetLoggedUsers(string roomName)
        {
            var loggedUser = GetByRoomName(roomName).Take(15);
            var users = new UserService().GetAll();

            var q = from lu in loggedUser
                    join u in users on lu.UserID equals u.UserID
                    select new UserView
                    {
                        UserName = u.UserName
                    };

            return q.ToList();
        }
    }
}

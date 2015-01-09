using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestChat.dal;
using TestChat.Model;

namespace TestChat.Business
{
    public interface IChatRoomService
    {
        IList<ChatRoom> GetAll();
        ChatRoom GetByRoomName(string roomName);
    }
    public class ChatRoomService : IChatRoomService
    {
        IChatRoomRepository repository;
        public ChatRoomService()
        {
            repository = new ChatRoomRepository();
        }
        public IList<ChatRoom> GetAll()
        {
            return repository.GetAll();
        }

        public ChatRoom GetByRoomName(string roomName)
        {
            return repository.GetSingle(r => r.ChatRoomName.Equals(roomName));
        }

        //Designed to have only one room at this point
        public string GetRoomName()
        {
            var chatRoom = GetAll().FirstOrDefault();

            if (chatRoom != null)
                return chatRoom.ChatRoomName;
            else
                return string.Empty;
        }

    }
}

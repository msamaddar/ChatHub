using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestChat.Model;

namespace TestChat.dal
{
    //User
    public interface IUserRepository : IGenericDataRepository<User>
    { }
    public class UserRepository : GenericDataRepository<User>, IUserRepository
    { }

    //Chat Posts
    public interface IChatPostRepository : IGenericDataRepository<ChatPost>
    { }
    public class ChatPostRepository : GenericDataRepository<ChatPost>, IChatPostRepository
    { }

    //Chat Room
    public interface IChatRoomRepository : IGenericDataRepository<ChatRoom>
    { }
    public class ChatRoomRepository : GenericDataRepository<ChatRoom>, IChatRoomRepository
    { }

    //Chat Room Users
    public interface IChatRoomUserRepository : IGenericDataRepository<ChatRoomUser>
    { }
    public class ChatRoomUserRepository : GenericDataRepository<ChatRoomUser>, IChatRoomUserRepository
    { }
}

using Domain.Models;
using MediatR;
 

namespace Application.Profiles.Queries
{
    public class GetAllProfiles:IRequest<ICollection<Profile>>
    {
    }
}

using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Widgets;
using Membership.Application.Queries.Widgets;
using Membership.Core.Consts;
using Membership.Core.DomainServices.Users;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Membership.Infrastructure.DAL.Exceptions;
using Membership.Infrastructure.DAL.Handlers.Users.Policies;
using Membership.Infrastructure.DAL.Handlers.Widgets.Policies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Widgets;

internal sealed class GetMyWidgetHandler : IQueryHandler<GetMyWidget, IEnumerable<WidgetDto>>
{
    private readonly MembershipDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IEnumerable<IWidgetPolicy> _widgetPolicies;
    private readonly IEnumerable<IUserDataRetrievePolicy> _userDataRetrievepolicies;

    public GetMyWidgetHandler(MembershipDbContext dbContext,
        IUserRepository userRepository, 
        IUserService userService,
        IEnumerable<IWidgetPolicy> widgetPolicies,
        IEnumerable<IUserDataRetrievePolicy> userDataRetrievepolicies)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _userService = userService;
        _widgetPolicies = widgetPolicies;
        _userDataRetrievepolicies = userDataRetrievepolicies;
    }

    public async Task<IEnumerable<WidgetDto>> HandleAsync(GetMyWidget query)
    {
        var user = await _userRepository.GetByIdAsync((Guid)query.UserId);
        
        var applicableUserRoles = await _userService.GetApplicableUserRolesAsync(user.Role, query.UserId);

        List<WidgetDto> widget = new();

        List<WidgetDetailDto> widgetDetails= new();
        
        if (user.Role == UserRole.CentralCommitteeAdmin())
        {
            // ====================== Users ======================
            var userCountByState = await _dbContext.Users
                .Include(x => x.State)
                .Where(x => x.Role == UserRole.StateAdmin())
                .GroupBy(x => x.StateId)
                .Select(x => new { StateId = x.Key, StateName = x.First().State.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalStateAdminUserCount = 0; 
            
            foreach(var item in userCountByState)
            {
                if (item.StateName is not null)
                {
                    totalStateAdminUserCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.StateName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }
            
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Users",
                SummaryValue = totalStateAdminUserCount,
                SummaryText = null,
                Details = widgetDetails
            });
            
            // ====================== Members ======================
            var memberCountByState = await _dbContext.Members
                .Include(x => x.State)
                .GroupBy(x => x.StateId)
                .Select(x => new { StateId = x.Key, StateName = x.First().State.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalStateMemberCount = 0; 
            widgetDetails= new();
            
            foreach(var item in memberCountByState)
            {
                if (item.StateName is not null)
                {
                    totalStateMemberCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.StateName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }
            
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Members",
                SummaryValue = totalStateMemberCount,
                SummaryText = null,
                Details = widgetDetails
            });
            
            // ====================== Dispute ======================
            var disputeRequestsCountByState = await _dbContext.DisputeRequests
                .Include(x => x.State)
                .Where(x => x.Status == DisputeStatus.Pending)
                .GroupBy(x => x.StateId)
                .Select(x => new { StateId = x.Key, StateName = x.First().State.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalStateDisputeRequestsCount = 0; 
            widgetDetails= new();
            
            foreach(var item in disputeRequestsCountByState)
            {
                if (item.StateName is not null)
                {
                    totalStateDisputeRequestsCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.StateName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }

            if (totalStateDisputeRequestsCount > 0)
            {
                widget.Add(new WidgetDto
                {
                    No = 1,
                    Title = "Total Dispute Requests",
                    SummaryValue = totalStateDisputeRequestsCount,
                    SummaryText = null,
                    Details = widgetDetails
                });
            }

            return widget;
        }
        
        if (user.Role == UserRole.StateAdmin())
        {
            // ====================== Users ======================
            var userCountByDistrict = await _dbContext.Users
                .Include(x => x.District)
                .Where(x => x.StateId == user.StateId && (x.Role == UserRole.DistrictAdmin() || x.Role == UserRole.DistrictAgent()))
                .GroupBy(x => x.DistrictId)
                .Select(x => new { DistrictId = x.Key, DistrictName = x.First().District.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalUserCount = 0; 
            
            foreach(var item in userCountByDistrict)
            {
                if (item.DistrictName is not null)
                {
                    totalUserCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.DistrictName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }
            
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Users",
                SummaryValue = totalUserCount,
                SummaryText = null,
                Details = widgetDetails
            });
            
            // ====================== Members ======================
            var memberCountByDistrict = await _dbContext.Members
                .Include(x => x.District)
                .Where(x => x.StateId == user.StateId)
                .GroupBy(x => x.DistrictId)
                .Select(x => new { DistrictId = x.Key, DistrictName = x.First().District.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalDistrictMemberCount = 0; 
            widgetDetails= new();
            
            foreach(var item in memberCountByDistrict)
            {
                if (item.DistrictName is not null)
                {
                    totalDistrictMemberCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.DistrictName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }
            
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Members",
                SummaryValue = totalDistrictMemberCount,
                SummaryText = null,
                Details = widgetDetails
            });
            
            // ====================== Dispute ======================
            var disputeRequestsCountByState = await _dbContext.DisputeRequests
                .Include(x => x.State)
                .Include(x => x.District)
                .Where(x => x.StateId == user.StateId && x.Status == DisputeStatus.Pending)
                .GroupBy(x => x.DistrictId)
                .Select(x => new { DistrictId = x.Key, DistrictName = x.First().District.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalDistrictDisputeRequestsCount = 0; 
            widgetDetails= new();
            
            foreach(var item in disputeRequestsCountByState)
            {
                if (item.DistrictName is not null)
                {
                    totalDistrictDisputeRequestsCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.DistrictName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }

            if (totalDistrictDisputeRequestsCount > 0)
            {
                widget.Add(new WidgetDto
                {
                    No = 1,
                    Title = "Total Dispute Requests",
                    SummaryValue = totalDistrictDisputeRequestsCount,
                    SummaryText = null,
                    Details = widgetDetails
                });
            }

            return widget;
        }
        
        if (user.Role == UserRole.DistrictAdmin())
        {
            // ====================== Users ======================
            var userCountByMandalam = await _dbContext.Users
                .Include(x => x.Mandalam)
                .Where(x => x.StateId == user.StateId && x.DistrictId == user.DistrictId && x.Role == UserRole.MandalamAgent())
                .GroupBy(x => x.MandalamId)
                .Select(x => new { MandalamId = x.Key, MandalamName = x.First().Mandalam.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalUserCount = 0; 
            
            foreach(var item in userCountByMandalam)
            {
                if (item.MandalamName is not null)
                {
                    totalUserCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.MandalamName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }
            
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Users",
                SummaryValue = totalUserCount,
                SummaryText = null,
                Details = widgetDetails
            });
            
            // ====================== Members ======================
            var memberCountByMandalam = await _dbContext.Members
                .Include(x => x.Mandalam)
                .Where(x => x.StateId == user.StateId && x.DistrictId == user.DistrictId)
                .GroupBy(x => x.MandalamId)
                .Select(x => new { MandalamId = x.Key, MandalamName = x.First().Mandalam.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalMandalamMemberCount = 0; 
            widgetDetails= new();
            
            foreach(var item in memberCountByMandalam)
            {
                if (item.MandalamName is not null)
                {
                    totalMandalamMemberCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.MandalamName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }
            
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Members",
                SummaryValue = totalMandalamMemberCount,
                SummaryText = null,
                Details = widgetDetails
            });
            
            // ====================== Dispute ======================
            var disputeRequestsCountByState = await _dbContext.DisputeRequests
                .Include(x => x.ProposedMandalam)
                .Where(x => x.StateId == user.StateId && x.DistrictId == user.DistrictId && x.Status == DisputeStatus.Pending)
                .GroupBy(x => x.ProposedMandalamId)
                .Select(x => new { MandalamId = x.Key, MandalamName = x.First().ProposedMandalam.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalMandalamDisputeRequestsCount = 0; 
            widgetDetails= new();
            
            foreach(var item in disputeRequestsCountByState)
            {
                if (item.MandalamName is not null)
                {
                    totalMandalamDisputeRequestsCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.MandalamName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }

            if (totalMandalamDisputeRequestsCount > 0)
            {
                widget.Add(new WidgetDto
                {
                    No = 1,
                    Title = "Total Dispute Requests",
                    SummaryValue = totalMandalamDisputeRequestsCount,
                    SummaryText = null,
                    Details = widgetDetails
                });
            }


            return widget;
        }

        if (user.Role == UserRole.DistrictAgent())
        {
            // ====================== Members ======================
            var memberCountByMandalam = await _dbContext.Members
                .Include(x => x.Mandalam)
                .Where(x => x.StateId == user.StateId && x.DistrictId == user.DistrictId)
                .GroupBy(x => x.MandalamId)
                .Select(x => new { MandalamId = x.Key, MandalamName = x.First().Mandalam.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalMandalamMemberCount = 0; 
            widgetDetails= new();
            
            foreach(var item in memberCountByMandalam)
            {
                if (item.MandalamName is not null)
                {
                    totalMandalamMemberCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.MandalamName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }
            
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Members",
                SummaryValue = totalMandalamMemberCount,
                SummaryText = null,
                Details = widgetDetails
            });
            
            // ====================== Dispute ======================
            var disputeRequestsCountByState = await _dbContext.DisputeRequests
                .Include(x => x.ProposedMandalam)
                .Where(x => x.StateId == user.StateId && x.DistrictId == user.DistrictId && x.Status == DisputeStatus.Pending)
                .GroupBy(x => x.ProposedMandalamId)
                .Select(x => new { MandalamId = x.Key, MandalamName = x.First().ProposedMandalam.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalMandalamDisputeRequestsCount = 0; 
            widgetDetails= new();
            
            foreach(var item in disputeRequestsCountByState)
            {
                if (item.MandalamName is not null)
                {
                    totalMandalamDisputeRequestsCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.MandalamName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }

            if (totalMandalamDisputeRequestsCount > 0)
            {
                widget.Add(new WidgetDto
                {
                    No = 1,
                    Title = "Total Dispute Requests",
                    SummaryValue = totalMandalamDisputeRequestsCount,
                    SummaryText = null,
                    Details = widgetDetails
                });
            }

            return widget;
        }
        
        if (user.Role == UserRole.MandalamAgent())
        {
            // ====================== Members ======================
            var memberCountByPanchayat = await _dbContext.Members
                .Include(x => x.Panchayat)
                .Where(x => x.StateId == user.StateId && x.MandalamId == user.MandalamId)
                .GroupBy(x => x.PanchayatId)
                .Select(x => new { PanchayatId = x.Key, PanchayatName = x.First().Panchayat.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalPanchayatMemberCount = 0; 
            widgetDetails= new();
            
            foreach(var item in memberCountByPanchayat)
            {
                if (item.PanchayatName is not null)
                {
                    totalPanchayatMemberCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.PanchayatName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }
            
            widget.Add(new WidgetDto
            {
                No = 1,
                Title = "Total Members",
                SummaryValue = totalPanchayatMemberCount,
                SummaryText = null,
                Details = widgetDetails
            });
            
            // ====================== Dispute ======================
            var disputeRequestsCountByState = await _dbContext.DisputeRequests
                .Include(x => x.ProposedPanchayat)
                .Where(x => x.StateId == user.StateId && x.ProposedMandalamId == user.MandalamId && x.Status == DisputeStatus.Pending)
                .GroupBy(x => x.ProposedPanchayatId)
                .Select(x => new { PanchayatId = x.Key, PanchayatName = x.First().ProposedPanchayat.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalPanchayatDisputeRequestsCount = 0; 
            widgetDetails= new();
            
            foreach(var item in disputeRequestsCountByState)
            {
                if (item.PanchayatName is not null)
                {
                    totalPanchayatDisputeRequestsCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.PanchayatName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }

            if (totalPanchayatDisputeRequestsCount > 0)
            {
                widget.Add(new WidgetDto
                {
                    No = 1,
                    Title = "Total Dispute Requests",
                    SummaryValue = totalPanchayatDisputeRequestsCount,
                    SummaryText = null,
                    Details = widgetDetails
                });
            }

            return widget;
        }

        if (user.Role == UserRole.DisputeCommittee())
        {
            var disputeRequestsCountByState = await _dbContext.DisputeRequests
                .Include(x => x.State)
                .Include(x => x.District)
                .Where(x => x.StateId == user.StateId && x.Status == DisputeStatus.Pending)
                .GroupBy(x => x.DistrictId)
                .Select(x => new { DistrictId = x.Key, DistrictName = x.First().District.Name, Count = x.Count() })
                .ToListAsync();
            
            Int32 totalDistrictDisputeRequestsCount = 0; 
            widgetDetails= new();
            
            foreach(var item in disputeRequestsCountByState)
            {
                if (item.DistrictName is not null)
                {
                    totalDistrictDisputeRequestsCount += item.Count;
                    widgetDetails.Add(new WidgetDetailDto
                    {
                        Text = item.DistrictName,
                        IntValue = item.Count,
                        TextValue = null
                    });
                }
            }

            if (totalDistrictDisputeRequestsCount > 0)
            {
                widget.Add(new WidgetDto
                {
                    No = 1,
                    Title = "Total Dispute Requests",
                    SummaryValue = totalDistrictDisputeRequestsCount,
                    SummaryText = null,
                    Details = widgetDetails
                });
            }
        }

        return widget;
    }
}
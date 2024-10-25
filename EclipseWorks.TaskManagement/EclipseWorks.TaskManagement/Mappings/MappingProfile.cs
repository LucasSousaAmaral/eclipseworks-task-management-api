using AutoMapper;
using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Presentation.DTOs;

namespace EW.TaskManagement.Presentation.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Project
        CreateMap<Project, ProjectDTO>();

        CreateMap<ProjectDTO, Project>();

        // TaskItem
        CreateMap<TaskItem, TaskItemDTO>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Histories, opt => opt.MapFrom(src => src.Histories));

        CreateMap<TaskItemDTO, TaskItem>();

        // Comment
        CreateMap<Comment, CommentDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));

        CreateMap<CommentDTO, Comment>();

        // TaskHistory
        CreateMap<TaskHistory, TaskHistoryDTO>()
            .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.Name));

        CreateMap<TaskHistoryDTO, TaskHistory>();

        // Create/Update
        CreateMap<CreateProjectDTO, Project>()
            .ConstructUsing(src => new Project(src.Name, src.UserId));

        CreateMap<CreateTaskItemDTO, TaskItem>()
            .ConstructUsing(src => new TaskItem(src.Title, src.Description, src.DueDate, src.Priority, src.ProjectId));

        CreateMap<UpdateTaskItemDTO, TaskItem>();
    }
}

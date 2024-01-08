using Domain.Entity;
using Domain.Response;
using Domain.ViewModel.Comments;

namespace Services.Interfaces
{
    public interface ICommentsService
    {
        Task<BaseResponse<CommentDeals>> Get(int id);
        Task<BaseResponse<bool>> Delete(int id);
        Task<BaseResponse<CommentDeals>> Update(CommentsVm model);
        Task<BaseResponse<bool>> Create(CommentsVm model);
        Task<BaseResponse<List<CommentDeals>>> GetAll(int dealId);
    }
}

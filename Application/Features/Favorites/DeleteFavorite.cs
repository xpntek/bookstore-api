using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Favorites;

public class DeleteFavorite
{
    public class DeleteFavoriteCommand : IRequest<Result<Favorite>>
    {
        public int Id { get; set; }
    }

    public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, Result<Favorite>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFavoriteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Favorite>> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            var favorite = await _unitOfWork.Repository<Favorite>().GetByIdAsync(request.Id);
            if (favorite is null)
            {
                return Results.NotFoundError(" Favorite book Id:" + request.Id);
            }
            
            _unitOfWork.Repository<Favorite>().Delete(favorite);
            var result =  await _unitOfWork.Complete();

            if (result < 0)
            {
                return Results.InternalError("Fail to delete favorite");
            }
            
            return _mapper.Map<Favorite>(favorite);
        }
    }
}
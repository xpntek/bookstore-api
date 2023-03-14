using Application.Dtos;
using Application.Errors;
using Application.Features.Books.BookSpecification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Persistence;

namespace Application.Features.Books;

public class UpdateBookAuthor
{
    public class UpdateBookAuthorCommand : IRequest<Result<BookDto>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PublishingCompany { get; set; }
        public string ISBN { get; set; }
        public string PublishingYear { get; set; }
        public string Edition { get; set; }
        public string PagesNumbers { get; set; }
        public int CategoryId { get; set; }
        public string Synopsis { get; set; }
        public float Price { get; set; }
        public int BookAmount { get; set; }
        public string CoverUrl { get; set; }
        public int Rating { get; set; }
        public List<Author> Authors { get; set; }
    }

    public class UpdateBookAuthorCommandHandler : IRequestHandler<UpdateBookAuthorCommand, Result<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UpdateBookAuthorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<BookDto>> Handle(UpdateBookAuthorCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(request.Id);
            if (book is null)
            {
                return Results.NotFoundError(" Book");   
            }
            var bookAuthorSpec = new FoundBookAuthorByBookIdSpecification(request.Id);
            var bookAuthorList = await _unitOfWork.Repository<BookAuthor>().ListWithSpecAsync(bookAuthorSpec);
            if (bookAuthorList is null)
            {
                return Results.NotFoundError(" Book author  ");
            }
            
            var listOfAuthorsModel = request.Authors;
            if (listOfAuthorsModel.Count == 0)
            {
                return Results.InternalError("The book don't have author");
            }

            var listOfAuthorsToValidateId = listOfAuthorsModel.Select(x => x.Id).ToList();

            var listOfAuthorsToValidateSpecification = new ListOfAuthorSpecification(listOfAuthorsToValidateId);

            var listOfAuthorsToValidate = await _unitOfWork.Repository<Author>()
                .ListWithSpecAsync(listOfAuthorsToValidateSpecification);

            if (listOfAuthorsToValidate is null)
            {
                return Results.NotFoundError("The book don't have author");
            }
            
            var categorySpec = new FoundCategoryByIdSpecification(request.CategoryId);
            var category = await _unitOfWork.Repository<Category>().GetEntityWithSpec(categorySpec);
            if (category is null)
            {
                return Results.NotFoundError("The category ");
            }

            await using var transactionScope = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                book.CategoryId = request.CategoryId;
                book.Edition = request.Edition;
                book.Price = request.Price;
                book.Rating = request.Rating;
                book.Synopsis = request.Synopsis;
                book.Title = request.Title;
                book.BookAmount = request.BookAmount;
                book.CoverUrl = request.CoverUrl;
                book.PagesNumbers = request.PagesNumbers;
                book.PublishingCompany = request.PublishingCompany;
                book.PublishingYear = request.PublishingYear;
                book.ISBN = request.ISBN;
                
                foreach (var bookAuthor in bookAuthorList)
                {
                    _unitOfWork.Repository<BookAuthor>().Delete(bookAuthor);
                }
                var result= await _unitOfWork.Complete() < 0;
                if (result)
                {
                    return Results.InternalError(" Book not save");
                }

                var bookAuthDto = new BookDto()
                {
                    CategoryId = category.Id,
                    Category = book.Category.Description,
                    Edition = request.Edition,
                    Price = request.Price,
                    Rating = request.Rating,
                    Synopsis = request.Synopsis,
                    Title = request.Title,
                    BookAmount = request.BookAmount,
                    CoverUrl = request.CoverUrl,
                    PagesNumbers = request.PagesNumbers,
                    PublishingCompany = request.PublishingCompany,
                    PublishingYear = request.PublishingYear,
                    ISBN = request.ISBN
                };
                
                var bookAuthorsList = new List<BookAuthor>();
                foreach (var author in listOfAuthorsModel)
                {
                    var bookAuthors = new BookAuthor()
                    {
                        AuthorId = author.Id,
                        BookId = book.Id,
                    };
                    bookAuthorsList.Add(bookAuthors);
                    _unitOfWork.Repository<BookAuthor>().Add(bookAuthors);
                    bookAuthDto.Authors.Add(author.FullName);
                }
                result= await _unitOfWork.Complete() < 0;
                if (result)
                {
                    return Results.InternalError(" Book not update");
                }
                
                await transactionScope.CommitAsync(cancellationToken);
                return bookAuthDto;
            }
            catch (Exception e)
            {
                await transactionScope.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
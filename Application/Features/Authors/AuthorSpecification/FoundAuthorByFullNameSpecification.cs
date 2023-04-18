using Application.Specifications;
using Domain;

namespace Application.Features.Authors.AuthorSpecification;

public class FoundAuthorByFullNameSpecification : BaseSpecification<Author>
{
    public FoundAuthorByFullNameSpecification(string fullName) : base(author => author.FullName == fullName)
    {
    }
}
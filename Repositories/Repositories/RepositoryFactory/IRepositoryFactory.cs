﻿using Queries;

namespace Repositories
{
    public interface IRepositoryFactory
    {
      //  IConfiguration Configuration { get; }
        IExecuters Executers { get; }
        ICommandText CommandText { get; }
        IPersonRepository PersonRepository { get; }
        //ITagRepository TagRepository { get; }
        //IArticleRepository ArticleRepository { get; }
        //IAuthorRepository AuthorRepository { get; }
        //IStateRepository StateRepository { get; }
        //IEntityStateRepository EntityStateRepository { get; }
        //ICommentRepository CommentRepository { get; }
        //ICategoryRepository CategoryRepository { get; }
        //IArticleCommentRepository ArticleCommentRepository { get; }
        //IArticleTagRepository ArticleTagRepository { get; }
    }
}

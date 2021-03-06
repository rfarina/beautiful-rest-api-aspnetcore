﻿using System.Threading.Tasks;
using BeautifulRestApi.Models;
using Mapster;

namespace BeautifulRestApi.Queries
{
    public class GetAllUsersQuery
    {
        private readonly BeautifulContext _context;
        private readonly PagedCollectionParameters _defaultPagingParameters;
        private readonly string _endpoint;

        public GetAllUsersQuery(BeautifulContext context, PagedCollectionParameters defaultPagingParameters, string endpoint)
        {
            _context = context;
            _defaultPagingParameters = defaultPagingParameters;
            _endpoint = endpoint;
        }

        public Task<PagedCollection<User>> Execute(PagedCollectionParameters parameters)
        {
            var collectionFactory = new PagedCollectionFactory<User>(PlaceholderLink.ToCollection(_endpoint));

            return collectionFactory.CreateFrom(
                _context.Users.ProjectToType<User>(),
                parameters.Offset ?? _defaultPagingParameters.Offset.Value,
                parameters.Limit ?? _defaultPagingParameters.Limit.Value);
        }
    }
}

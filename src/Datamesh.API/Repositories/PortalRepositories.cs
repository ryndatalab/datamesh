/********************************************************************************
 * Copyright (c) 2021, 2023 BMW Group AG
 * Copyright (c) 2021, 2023 Contributors to the Eclipse Foundation
 *
 * See the NOTICE file(s) distributed with this work for additional
 * information regarding copyright ownership.
 *
 * This program and the accompanying materials are made available under the
 * terms of the Apache License, Version 2.0 which is available at
 * https://www.apache.org/licenses/LICENSE-2.0.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 *
 * SPDX-License-Identifier: Apache-2.0
 ********************************************************************************/

using Microsoft.EntityFrameworkCore;
using PortalBackend.DBAccess.Repositories.IsoPay;
using System.Collections.Immutable;

namespace Datamesh.API.Repositories;

public class PortalRepositories : Org.Eclipse.TractusX.Portal.Backend.PortalBackend.DBAccess.IPortalRepositories
{
    private readonly PortalDbContext _dbContext;

    private static readonly IReadOnlyDictionary<Type, Func<PortalDbContext, object>> _types = new Dictionary<Type, Func<PortalDbContext, object>> {


        { typeof(IIsoPayRepository), context => new IsoPayRepository(context) },

    }.ToImmutableDictionary();

    public PortalRepositories(PortalDbContext portalDbContext)
    {
        _dbContext = portalDbContext;
    }

    public RepositoryType GetInstance<RepositoryType>()
    {
        object? repository = default;

        if (_types.TryGetValue(typeof(RepositoryType), out var createFunc))
        {
            repository = createFunc(_dbContext);
        }
        return (RepositoryType)(repository ?? throw new ArgumentException($"unexpected type {typeof(RepositoryType).Name}", nameof(RepositoryType)));
    }

    /// <inheritdoc />
    public TEntity Attach<TEntity>(TEntity entity, Action<TEntity>? setOptionalParameters = null) where TEntity : class
    {
        var attachedEntity = _dbContext.Attach(entity).Entity;
        setOptionalParameters?.Invoke(attachedEntity);

        return attachedEntity;
    }

    public void AttachRange<TEntity>(IEnumerable<TEntity> entities, Action<TEntity> setOptionalParameters) where TEntity : class
    {
        foreach (var entity in entities)
        {
            var attachedEntity = _dbContext.Attach(entity).Entity;
            setOptionalParameters.Invoke(attachedEntity);
        }
    }

    public IEnumerable<TEntity> AttachRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        foreach (var entity in entities)
        {
            yield return _dbContext.Attach(entity).Entity;
        }
    }

    /// <inheritdoc />
    public TEntity Remove<TEntity>(TEntity entity) where TEntity : class
        => _dbContext.Remove(entity).Entity;

    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        => _dbContext.RemoveRange(entities);

    public Task<int> SaveAsync()
    { 
        return _dbContext.SaveChangesAsync();
       
    }
    public void Clear() => _dbContext.ChangeTracker.Clear();
}

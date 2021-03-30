﻿using System.Threading.Tasks;

namespace Volo.Abp.MultiLingualObject
{
    public interface IMultiLingualObjectManager
    {
        Task<TTranslation> GetTranslationAsync<TMultiLingual, TTranslation>(
            TMultiLingual multiLingual,
            string culture = null,
            bool fallbackToParentCultures = true)
            where TMultiLingual : IHasMultiLingual<TTranslation>
            where TTranslation : class, IMultiLingualTranslation;
    }
}

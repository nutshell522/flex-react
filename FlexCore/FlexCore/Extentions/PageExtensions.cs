﻿using AutoMapper;
using FlexCore.Models;

namespace FlexCore.Extentions
{
	public static class PageExtensions
	{
		/// <summary>
		/// 用於轉換 Page 內的 Items
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TDestination"></typeparam>
		/// <param name="sourcePage"></param>
		/// <param name="mapper"></param>
		/// <returns></returns>
		public static Page<TDestination> MapTo<TSource, TDestination>(this Page<TSource> sourcePage, IMapper mapper)
		{
			IEnumerable<TDestination> mappedItems = mapper.Map<IEnumerable<TDestination>>(sourcePage.Items);
			return new Page<TDestination>(mappedItems, sourcePage.TotalCount, sourcePage.PageIndex, sourcePage.PageSize);
		}
	}
}

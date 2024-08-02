﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shoopi.Data;
using Shoopi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
	public class ProductDAO
	{
		private readonly ShoopiContext _context;
		private static ProductDAO instance = null;
		private ProductDAO()
		{
			_context = new ShoopiContext();
		}

		public static ProductDAO Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ProductDAO();
				}
				return instance;
			}
		}


		public async Task<List<Product>> GetProducts(int? type, string query)
		{
			var products = _context.Products.AsQueryable();

			if (!string.IsNullOrEmpty(query))
			{
				products = products.Where(p => p.ProductName.Contains(query));
			}

			if (type.HasValue)
			{
				products = products.Where(p => p.TypeId == type.Value);
			}
			return await products.ToListAsync();
		}

		public async Task<Product> GetProductById(int id)
		{
			return await _context.Products.Include(x => x.Type).FirstOrDefaultAsync(p => p.ProductId == id);
		}
	}
}
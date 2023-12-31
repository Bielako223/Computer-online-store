﻿using StoreLibrary.Models;

namespace StoreLibrary.DataAccess
{
    public interface IitemData
    {
        Task AddItem(ItemModel model);
        Task DeleteItem(int id);
        Task<List<ItemModel>> GetAllItems();
        Task<List<ItemModel>> GetSearchedItems(SearchingModel model);
        Task<ItemModel> GetItemById(int id);
        Task UpdateItem(ItemModel model);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Data.Model;
using CourseWork.Data;
using System.Text.Json;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;

namespace CourseWork.Data
{
    public class InventoryService
    {


     
        public static List<InventoryModel> GetAllItems()
        {
            string appItemsFilePath = Utility.GetAppItemsFilePath();
            if (!File.Exists(appItemsFilePath))
            {
                return new List<InventoryModel>();
            }

            var json = File.ReadAllText(appItemsFilePath);

            return JsonSerializer.Deserialize<List<InventoryModel>>(json);
        }
        private static void SaveAll(Guid userId, List<InventoryModel> inventory)
        {
            string appDataDirectoryPath = Utility.GetAppDirectoryPath();
            string itemFilePath = Utility.GetAppItemsFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(inventory);
            File.WriteAllText(itemFilePath, json);
        }
        public static List<InventoryModel> CreateItem(Guid ItemId, string Item,  int Quantity, string ApprovedBy, string TakenBy, DateTime DateTakenOut, double Price)
        {

            {
                DateTime currentTime = DateTime.Now;

                // Check if it is a weekday (Monday through Friday)
                //if (currentTime.DayOfWeek >= DayOfWeek.Monday && currentTime.DayOfWeek <= DayOfWeek.Friday)
                //{
                    // Check if the current time is between 9:00 AM and 4:00 PM
                    //if (currentTime.TimeOfDay >= new TimeSpan(9, 0, 0) && currentTime.TimeOfDay <= new TimeSpan(16, 0, 0))
                    //{
                        var inventory = GetAllItems();
                        var item = new InventoryModel()
                        {
                            ItemId = ItemId,
                            Item = Item,
                            Quantity = Quantity,
                            ApprovedBy = ApprovedBy,
                            TakenBy = TakenBy,
                            DateTakenOut = DateTakenOut,
                            Price = Price
                        };
                        inventory.Add(item);

                        SaveAll(item.ItemId, inventory);
                        return inventory;
                    //}
                   //else
                    //{
                        // The current time is not within the allowed range, so we cannot create the item
                        
                        //throw new Exception("Item creation is only allowed between 9:00 AM and 4:00 PM on weekdays.");
                    //}
                //}
                //else
                //{
                    // It is not a weekday, so we cannot create the item
                  //  throw new Exception("Item creation is only allowed on weekdays.");
               // }
            }
        }
        public static List<InventoryModel> InventoryDetails()
        {
            List<InventoryModel> inventory = new List<InventoryModel>()
            {
                new InventoryModel
                {
                                ItemId = Guid.NewGuid(),
                                Item = "Wheels",
                                Quantity = 10,
                                ApprovedBy = "Staff",
                                TakenBy = "Admin",
                                DateTakenOut = DateTime.UtcNow,
                                Price = 1000
                            },
                   new InventoryModel
                {
                                ItemId = Guid.NewGuid(),
                                Item = "Arrow Screws",
                                Quantity = 2,
                                ApprovedBy = "Staff",
                                TakenBy = "Admin",
                                DateTakenOut = DateTime.UtcNow,
                                Price = 4000
                            }


                };
            return inventory;
            
            

        }
        public static void UpdateItem(Guid ItemId, string Item, int Quantity, string ApprovedBy, string TakenBy, DateTime DateTakenOut, double Price)
        {
            var inventory = GetAllItems();
            var item = inventory.FirstOrDefault(x => x.ItemId == ItemId);
            if (item == null)
            {
                throw new Exception("Item not found");
            }

            item.Item = Item;
            item.Quantity = Quantity;
            item.ApprovedBy = ApprovedBy;
            item.TakenBy = TakenBy;
            item.DateTakenOut = DateTakenOut;
            item.Price = Price;

            SaveAll(item.ItemId, inventory);
        }
        public static void DeleteItem(Guid ItemId)
        {
            var inventory = GetAllItems();
            var item = inventory.FirstOrDefault(x => x.ItemId == ItemId);
            if (item == null)
            {
                throw new Exception("Item not found");
            }

            inventory.Remove(item);

            SaveAll(item.ItemId, inventory);
        }

        
        public static List<RequestModel> GetAllRequests()
        {
            string appRequestFilePath = Utility.GetAppRequestFilePath();
            if (!File.Exists(appRequestFilePath))
            {
                return new List<RequestModel>();
            }

            var json = File.ReadAllText(appRequestFilePath);

            return JsonSerializer.Deserialize<List<RequestModel>>(json);
        }
        private static void SaveAllRequest(Guid userId, List<RequestModel> inventory)
        {
            string appDataDirectoryPath = Utility.GetAppDirectoryPath();
            string requestFilePath = Utility.GetAppRequestFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(inventory);
            File.WriteAllText(requestFilePath, json);
        }
        public static List<RequestModel> CreateRequest(string Item, int Quantity, string TakenBy, DateTime DateTakenOut)
        {

            {
               
                var request = GetAllRequests();
                var item = new RequestModel()
                {
                    RequestId = Guid.NewGuid(),
                    RequestItem = Item,
                    Quantity = Quantity,
                    TakenBy = TakenBy,
                    DateTakenOut = DateTakenOut,
                    status = Status.Pending,
                    done = false

                };
                        request.Add(item);

                        SaveAllRequest(item.RequestId, request);
                        return request;
                   
            }
        }
        public static void updateStatus(Guid RequestId, Status status,String username,bool done)
        {
            var request = GetAllRequests();
          
            var item = request.FirstOrDefault(x => x.RequestId == RequestId);
            if (item == null)
            {
                throw new Exception("Request not found");
            }
            item.ApprovedBy = username;
            item.status = status;
            item.done = done;
            if (item.status == Status.Approved) { 
            changeQuantity(item.RequestItem, item.Quantity);
            }
            SaveAllRequest(item.RequestId, request);
        }
        public static void changeQuantity(string requestitem, int Quantity)
        {
            var inventory = GetAllItems();
            var item = inventory.FirstOrDefault(x => x.Item == requestitem);
            if (item == null)
            {
                throw new Exception("Item not found");
            }

            item.Quantity -=Quantity;


            SaveAll(item.ItemId, inventory);
            
        }
        public static RequestModel retrieveRequest(Guid RequestId)
        {
            var request = GetAllRequests();
            var item = request.FirstOrDefault(x => x.RequestId == RequestId);
            if (item == null)
            {
                throw new Exception("Request not found");
            }
            return item;
        }

   
      
    }
    
}



﻿using BuisnessLibrary.Bl.Repository.Interface;
using BuisnessLibrary.Utilities;
using DomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Bl.Repository
{
    public class ItemImageRepository : GenericRepository<TbItemImage>, IItemImageRepository
    {
        private readonly AppDbContext _context;

        public ItemImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// able to add ,update and remove images
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="uploadedImageNames"></param>
        /// <returns></returns>
        public async Task UploadImages(int itemId, List<string> uploadedImageNames)
        {
            // Step 1: Retrieve existing images from the database for the specified item
            var existingImages = await FindAsync(im => im.ItemId == itemId);
             
            // Step 2: Determine which images to keep, add, or delete
            var existingImageNames = existingImages.Select(img => img.ImageName).ToList();

            // Keep existing images
            var imagesToKeep = existingImages.Where(img => uploadedImageNames.Contains(img.ImageName)).ToList();

            // Add new images
            var newImages = new List<TbItemImage>();
            int i = 1;
            foreach (var uploadedImageName in uploadedImageNames.Where(name => !existingImageNames.Contains(name)))
            {
                // Generate a unique name using the helper function
                var newImageName = await Helper.UploadImage(uploadedImageName, "Items"); // Assuming "Items" is the folder
                newImageName = "UTemp" + i;
                if (!string.IsNullOrEmpty(newImageName))
                {
                    newImages.Add(new TbItemImage
                    {
                        ImageName = newImageName, // Use the new name generated by the helper
                        ItemId = itemId
                    });
                }
                i++;//temp method just to find a better solution 
            }

            // Determine images to delete
            var imagesToDelete = existingImages
                .Where(img => !uploadedImageNames.Contains(img.ImageName))
                .ToList();

            // Step 3: Update the database context
            // Remove images that are not present in the uploaded list
            if (imagesToDelete.Any())
            {
                _context.RemoveRange(imagesToDelete);
            }

            // Add new images
            if (newImages.Any())
            {
                await AddRangeAsync(newImages);
            }
        }


    }
}

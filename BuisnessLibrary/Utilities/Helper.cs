using Microsoft.AspNetCore.Http;


namespace BuisnessLibrary.Utilities
{
    public class Helper
    {


        // Method to generate a unique GUID-based file name
        public static string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        // Method to ensure that the folder exists
        public static bool CreateFolderIfDoesNotExist(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    // Log the exception if necessary
                    return false;
                }
            }
            return true;
        }

        // Method to replace the original file name with a GUID
        public static string ReplaceFileNameWithGUID(string originalFileName)
        {
            string fileExtension = Path.GetExtension(originalFileName);
            return GenerateGUID() + fileExtension;
        }

        // Method to save the uploaded image to a specific folder
        public static async Task<string> SaveImageToFolderAsync(IFormFile file, string destinationFolder)
        { string newFileName="";
            // Ensure the destination folder exists
            if (!CreateFolderIfDoesNotExist(destinationFolder))
            {
                return newFileName;
            }

            // Create a new file name using GUID
            newFileName = ReplaceFileNameWithGUID(file.FileName);

            // Construct the full destination path
            string fullFilePath = Path.Combine(destinationFolder, newFileName);

            try
            {
                // Save the file asynchronously to the destination
                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return newFileName;
            }

            return newFileName;
        }


    public static async Task<string> UploadImage(string fileName, string folderName)
        {
          
                if (!string.IsNullOrEmpty(fileName))
                {
                    // Generate a unique image name, keeping the original extension if necessary.
                    string imageName = Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyyMMdd") + ".jpg";

                    // Define the target file path for the image in the given folder.
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\" + folderName, imageName);

                    // Check if the image file already exists in your server and proceed to copy or move it.
                    // Here you should either copy the file from a source or perform the necessary operation.
                    // Assuming the files are stored in a temporary folder:
                    var tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Temp\", fileName);

                    if (File.Exists(tempFilePath))
                    {
                        using (var sourceStream = new FileStream(tempFilePath, FileMode.Open))
                        using (var destinationStream = new FileStream(filePaths, FileMode.Create))
                        {
                            await sourceStream.CopyToAsync(destinationStream);
                        }
                        return imageName;
                    }
                }

            return string.Empty;
        }
    }
}

namespace Lumia.Utilites.Extensitions
{
    public static class FileValidator
    {
        public static bool ValidateType(this IFormFile file,string type)
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }
        public static bool ValidateSize(this IFormFile file, int kb )   
        {
            if (file.Length<kb*1024)
            {
                return true;
            }
            return false;
        }
        public static async Task<string>CreateAsync(this IFormFile file,string root,params string[]folders)
        {
            string FileName=Guid.NewGuid().ToString()+file.FileName;
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, FileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return FileName;
        }
        public static void DelteAsyncc(this string  filename, string root, params string[] folders)
        {
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, filename);
            if (File.Exists(path))
            {
                File.Delete(path);  
            }
        }
    }
}

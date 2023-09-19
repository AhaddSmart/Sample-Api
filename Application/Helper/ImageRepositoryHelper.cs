using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Application.Helpers
{
    public class ImageRepositoryHelper
    {
        private readonly IApplicationDbContext _context;
        public ImageRepositoryHelper(IApplicationDbContext context)
        {
            _context = context;

        }


        public async Task<int> SaveImage(IFormFile? file, string FileName, int Position, FileRepositoryTableRef TableRefernce, int TableRefID, CancellationToken cancellationToken)
        {
            string filePath = "";
            if (file != null && file.Length > 0)
            {
                //file Saving Code
                Guid newGuid = Guid.NewGuid();
                string Extension = GetExtension(file.FileName);

                string uploads = "Resources/uploads/" + TableRefernce;
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                filePath = uploads + "/" + newGuid.ToString() + Extension;
            }
            else
            {
                filePath = getDefaultImage(TableRefernce);
            }


            var entity = new FileRepo
            {
                fileName = FileName,
                filePath = filePath,
                filePosition = Position,
                TableRef = TableRefernce,
                tableRefID = TableRefID,
                type = file.ContentType != null ? file.ContentType : "",
            };

            if (file != null && file.Length > 0)
            {
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            await _context.FileRepos.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
//        public async Task<int> UpdateImage(int ImageRepoID, IFormFile? file, string FileName, int Position, string type, FileRepositoryTableRef TableRefernce, int TableRefID, CancellationToken cancellationToken)
        public async Task<int> UpdateImage(int ImageRepoID, IFormFile? file, string FileName, int Position, FileRepositoryTableRef TableRefernce, int TableRefID, CancellationToken cancellationToken)

        {
            string filePath = "";
            string oldFileName = "";
            if (file != null && file.Length > 0)
            {
                //file Saving Code
                Guid newGuid = Guid.NewGuid();
                string Extension = GetExtension(file.FileName);

                string uploads = "Resources/uploads/" + TableRefernce;
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                filePath = uploads + "/" + newGuid.ToString() + Extension;


                var ImageRepoData = await _context.FileRepos
                   .FindAsync(new object[] { ImageRepoID }, cancellationToken);
                if (ImageRepoData != null)
                {
                    oldFileName = ImageRepoData.filePath;
                    ImageRepoData.fileName = FileName;
                    ImageRepoData.filePath = filePath;
                    ImageRepoData.filePosition = Position;
                    ImageRepoData.TableRef = TableRefernce;
                    ImageRepoData.tableRefID = TableRefID;
                    //ImageRepoData.type = type;
                    ImageRepoData.type = file.ContentType != null ? file.ContentType : "";

                    if (File.Exists(oldFileName))
                    {
                        File.Delete(oldFileName);
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                   

                    return ImageRepoData.Id;

                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return 0;
            }
        }

        //public async Task<bool> DeleteImage(int ImageRepoId, CancellationToken cancellationToken)
        public async Task<bool> DeleteImage(int fileRepoID, CancellationToken cancellationToken)
        {
            try
            {
             
                var objimagerepo = await _context.FileRepos
                .FindAsync(new object[] { fileRepoID }, cancellationToken);
                if (objimagerepo != null)
                {
                    _context.FileRepos.Remove(objimagerepo);
                   
                    if (File.Exists(objimagerepo.filePath))
                    {
                        File.Delete(objimagerepo.filePath);    
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        private string getDefaultImage(FileRepositoryTableRef tableRefernce)
        {
            string uploads = "Resources/uploads/images/DefaultImages";

            return uploads + "/" + tableRefernce.ToString() + ".png";
        }

        private string GetExtension(string fileName)
        {
            string[] Files = fileName.Split('.');
            return "." + Files[1];
        }

    }
}

﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TARge21Shop.Core.Domain;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;

namespace TARge21Shop.ApplicationServices.Services
{
    public class FilesServices : IFilesServices
    {
        private readonly TARge21ShopContext _context;

        public FilesServices
            (
                TARge21ShopContext context
            )
        {
            _context = context;
        }

        public void UploadFileToDatabase(SpaceshipDto dto, Spaceship domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var photo in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = photo.FileName,
                            SpaceshipId = domain.Id,

                        };

                        photo.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.FileToDatabase.Add(files);
                    }
                }

            }
            
            
        }

        public async Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto)
        {
            var image = await _context.FileToDatabase
                .Where(X => X.Id == dto.Id)
                .FirstOrDefaultAsync();

            _context.FileToDatabase.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<List<FileToDatabase>> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var image = await _context.FileToDatabase
                    .Where(X => X.Id == dto.Id)
                    .FirstOrDefaultAsync();

                _context.FileToDatabase.Remove(image);
                await _context.SaveChangesAsync();
            }

            return null;
        }
    }
}

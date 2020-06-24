using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Services
{
    public class ResetService
    {
        private ResetTokenRepository _resetTokenRepository;

        public ResetService(MongoClient db)
        {
            _resetTokenRepository = new ResetTokenRepository(db);
        }

        public bool InsertToken(PasswordResetToken token)
        {
            throw new NotImplementedException();
        }

        public bool DeleteToken()
        {
            throw new NotImplementedException();
        }

        public bool UpdateToken(PasswordResetToken token)
        {
            return _resetTokenRepository.UpdateToken(token);
        }

        public PasswordResetToken GetToken(string token)
        {
            return _resetTokenRepository.GetToken(token);
        }

        public ICollection<PasswordResetToken> GetTokensByUserId(ObjectId userId)
        {
            return _resetTokenRepository.GetTokens(userId);
        }
    }
}

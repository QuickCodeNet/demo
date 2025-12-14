using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Payment;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.Payment
{
    public partial interface IPaymentService
    {
        Task<Response<PaymentDto>> InsertAsync(PaymentDto request);
        Task<Response<bool>> DeleteAsync(PaymentDto request);
        Task<Response<bool>> UpdateAsync(int id, PaymentDto request);
        Task<Response<List<PaymentDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<PaymentDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetPendingPaymentsResponseDto>>> GetPendingPaymentsAsync(PaymentStatus paymentsStatus);
        Task<Response<List<GetOrdersForPaymentsResponseDto>>> GetOrdersForPaymentsAsync(int paymentsId);
        Task<Response<GetOrdersForPaymentsResponseDto>> GetOrdersForPaymentsDetailsAsync(int paymentsId, int ordersId);
        Task<Response<int>> MarkPaymentFailedAsync(PaymentStatus paymentsStatus, MarkPaymentFailedRequestDto updateRequest);
    }
}
using KoiDeliveryOrderingSystem.Common;
using KoiDeliveryOrderingSystem.Data;
using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Service
{
    public class ShipmentOrderModel
    {
        public int? CustomerId { get; set; }

        public int? PricingId { get; set; }

        public DateTime? OrderDate { get; set; }

        public string OriginLocation { get; set; }

        public string DestinationLocation { get; set; }

        public decimal? TotalWeight { get; set; }

        public int? TotalQuantity { get; set; }

        public string ShipmentMethod { get; set; }

        public string AdditionalServices { get; set; }

        public decimal? AdditionalFee { get; set; }

        public string OrderStatus { get; set; }
    }
    public interface IShipmentOrderService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Create(ShipmentOrder shipmentOrder);
        Task<IBusinessResult> Update(ShipmentOrder shipmentOrder);
        Task<IBusinessResult> Save(ShipmentOrder shipmentOrder);
        Task<IBusinessResult> DeleteById(int id);
    }
    public class ShipmentOrderService : IShipmentOrderService
    {
        public readonly UnitOfWork _unitOfWork;
        public ShipmentOrderService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> Create(ShipmentOrder shipmentOrder)
        {
            if (shipmentOrder == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                var newOrder = new ShipmentOrder
                {
                    CustomerId = shipmentOrder.CustomerId,
                    PricingId = shipmentOrder.PricingId,
                    OrderDate = shipmentOrder.OrderDate,
                    OriginLocation = shipmentOrder.OriginLocation,
                    DestinationLocation = shipmentOrder.DestinationLocation,
                    TotalWeight = shipmentOrder.TotalWeight,
                    TotalQuantity = shipmentOrder.TotalQuantity,
                    ShipmentMethod = shipmentOrder.ShipmentMethod,
                    AdditionalServices = shipmentOrder.AdditionalServices,
                    AdditionalFee = shipmentOrder.AdditionalFee,
                    OrderStatus = shipmentOrder.OrderStatus,
                };
                var result = await _unitOfWork.ShipmentOrderRepository.CreateAsync(newOrder);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, newOrder);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            var shipmentOrder = await _unitOfWork.ShipmentOrderRepository.GetByIdAsync(id);
            if (shipmentOrder == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                var result = await _unitOfWork.ShipmentOrderRepository.RemoveAsync(shipmentOrder);
                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, shipmentOrder);
                }
            }
        }

        public async Task<IBusinessResult> GetAll()
        {
            var shipmentOrders = await _unitOfWork.ShipmentOrderRepository.GetAllAsync();
            if (shipmentOrders == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, shipmentOrders);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var shipmentOrder = await _unitOfWork.ShipmentOrderRepository.GetByIdAsync(id);
            if (shipmentOrder == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, shipmentOrder);
            }
        }

        public async Task<IBusinessResult> Save(ShipmentOrder shipmentOrder)
        {
            try
            {
                if (shipmentOrder.OrderId != 0) 
                {
                    var shipmentOrderTmp = await _unitOfWork.ShipmentOrderRepository.GetByIdAsync(shipmentOrder.OrderId);

                    if (shipmentOrderTmp != null)
                    {
                        shipmentOrderTmp.CustomerId = shipmentOrder.CustomerId;
                        shipmentOrderTmp.PricingId = shipmentOrder.PricingId;
                        shipmentOrderTmp.OrderDate = shipmentOrder.OrderDate;
                        shipmentOrderTmp.OriginLocation = shipmentOrder.OriginLocation;
                        shipmentOrderTmp.DestinationLocation = shipmentOrder.DestinationLocation;
                        shipmentOrderTmp.TotalWeight = shipmentOrder.TotalWeight;
                        shipmentOrderTmp.TotalQuantity = shipmentOrder.TotalQuantity;
                        shipmentOrderTmp.ShipmentMethod = shipmentOrder.ShipmentMethod;
                        shipmentOrderTmp.AdditionalServices = shipmentOrder.AdditionalServices;
                        shipmentOrderTmp.AdditionalFee = shipmentOrder.AdditionalFee;
                        shipmentOrderTmp.OrderStatus = shipmentOrder.OrderStatus;

                        var result = await _unitOfWork.ShipmentOrderRepository.UpdateAsync(shipmentOrderTmp);
                        if (result > 0)
                        {
                            return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, shipmentOrderTmp);
                        }
                        else
                        {
                            return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                        }
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, "Shipment order not found.");
                    }
                }
                else 
                {
                    var result = await _unitOfWork.ShipmentOrderRepository.CreateAsync(shipmentOrder);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, shipmentOrder);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }

            // Ensure that a return statement exists for all code paths
            return new BusinessResult(Const.FAIL_UPDATE_CODE, "Unknown error occurred.");
        }


        public Task<IBusinessResult> Update(ShipmentOrder shipmentOrder)
        {
            throw new NotImplementedException();
        }
    }
}

IF EXISTS(SELECT 1 FROM sys.procedures 
          WHERE Name = 'sp_getallorders')
BEGIN
    DROP PROCEDURE dbo.sp_getallorders
END
GO
CREATE PROCEDURE sp_getallorders          
@Page INT,          
@Size INT,      
@orderstatus int      
AS          
BEGIN          
 CREATE TABLE #ORDERS(          
 OrderId INT,          
 ProductRefId INT,          
 OrderItemId INT,          
 Name VARCHAR(MAX),          
 Description VARCHAR(MAX),          
 Price FLOAT,          
 Image VARCHAR(MAX),          
 CreateDate DATETIME,          
 ProductOptionRefId INT,          
 Quantity INT,      
 DeliveryCharges INT,          
 TotalAmount FLOAT,          
 PaymentStatus BIT,          
 OrderStatus VARCHAR(MAX),          
 OrderStatusId INT,        
 UserId int,        
 FirstName varchar(max),        
 Email varchar(max),      
 Total int      
 )          
   if @orderstatus>0       
   BEGIN      
   Insert into #ORDERS(OrderId,ProductRefId,OrderItemId,Name,Description,Price,Image,CreateDate,ProductOptionRefId,Quantity,DeliveryCharges,          
 TotalAmount,PaymentStatus,OrderStatus,OrderStatusId,UserId,FirstName,Email,Total)          
 SELECT o.OrderId,ot.ProductRefId,ot.OrderItemDetailId,p.Name,p.Description,p.Price,p.ImageUrl,ot.CreateDate,ot.ProductOptionId,ot.Quantity,o.DeliveryCharges,          
 ot.TotalAmount,ot.PaymentStatus,s.OrderStatusDesc,ot.OrderStatusRefId,u.UserId,u.FirstName,u.Email,(select Count(*) from TblOrders join TblOrderItemDetails       
 on TblOrders.OrderId=TblOrderItemDetails.OrderRefId where TblOrders.IsActive=1 and TblOrderItemDetails.OrderStatusRefId=@orderstatus) FROM  TblOrders O          
 JOIN TblOrderItemDetails ot          
 on o.OrderId=ot.OrderRefId          
 JOIN TblProducts p          
 on ot.ProductRefId=p.ProductId          
 join TblOrderStatusCodes s          
 on ot.OrderStatusRefId=s.StatusCodeId          
 join TblUsers u          
 on o.UserRefId=u.UserId          
 where o.IsActive=1 AND ot.OrderStatusRefId=@orderstatus        
 order by O.OrderId desc          
 OFFSET (@Page -1) * @Size ROWS          
 FETCH NEXT @Size ROWS ONLY        
   END      
   ELSE      
   BEGIN      
 Insert into #ORDERS(OrderId,ProductRefId,OrderItemId,Name,Description,Price,Image,CreateDate,ProductOptionRefId,Quantity,DeliveryCharges,          
 TotalAmount,PaymentStatus,OrderStatus,OrderStatusId,UserId,FirstName,Email,Total)          
 SELECT o.OrderId,ot.ProductRefId,ot.OrderItemDetailId,p.Name,p.Description,p.Price,p.ImageUrl,ot.CreateDate,ot.ProductOptionId,ot.Quantity,o.DeliveryCharges,          
 ot.TotalAmount,ot.PaymentStatus,s.OrderStatusDesc,ot.OrderStatusRefId,u.UserId,u.FirstName,u.Email,(select Count(*) from TblOrders where TblOrders.IsActive=1)      
 FROM  TblOrders O          
 JOIN TblOrderItemDetails ot          
 on o.OrderId=ot.OrderRefId          
 JOIN TblProducts p          
 on ot.ProductRefId=p.ProductId          
 join TblOrderStatusCodes s          
 on ot.OrderStatusRefId=s.StatusCodeId          
 join TblUsers u          
 on o.UserRefId=u.UserId          
 where o.IsActive=1          
 order by O.OrderId desc          
 OFFSET (@Page -1) * @Size ROWS          
 FETCH NEXT @Size ROWS ONLY        
 END      
 SELECT * FROM #ORDERS          
DROP TABLE #ORDERS          
END 
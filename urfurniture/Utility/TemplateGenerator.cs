using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using urfurniture.DAL.Repository;
using urfurniture.Models;
using Path = urfurniture.Models.Path;

namespace urfurniture.Utility
{
    public static class TemplateGenerator
    {
       
        public static string GetHTMLString(InvoiceModel invoceObj, string filepath )
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@" <html>
<head>
</head> 
<body>
                                          <div class='col-xl-12 col-lg-12'> 
                                         <div class='row invoice-container' id='invoice-wrapper'>
                                               <div id = 'container'> 
                               <section id='memo'> 
                                                     <div class='company-name'> <span>Ur-furniture</span>
                                                         <div class='right-arrow'>
                                                         </div> 
                                                     </div> 
                                                     <div class='logo'> <img data-logo='company_logo'/> </div> 
                                                     <div class='company-info'> 
                                                        <div> <span>211a Wednesbury Road</span> <span>Walsall, WS2 9QL</span>
                                                        </div> 
                                                        <div>www.urfurniture.co.uk</div> 
                                                        <div>+44 7474500000</div> 
                                                     </div> 
                                </section> 
                                <section id = 'invoice-info'>
                                   <div> 
                                       <span> Order Date</span> 
                                       <span>Invoice Date</span> 
                                       <span>Payment Status</span> 
                                       <span>Invoice No #</span> 
                                  </div>     
                              <div>
                                <span> {0}</span>
                                <span> {1}</span>
                                <span> success </span>
                                <span>{2} </span>
                             </div>
                               </section>


<section id = 'client-info'>

<span> Bill to:</span>

<div>

<span class= 'bold'>{3} </span>

</div>

<div>

<span>{4}</span>

</div>


<div>

<span>{5} 
        {6} </span>

</div>


<div>

<span>{7} </span>

</div>


<div>

<span>{8} </span>

</div>

</section>




<div class= 'clearfix'></div>


<section id = 'invoice-title-number'>


<span id = 'title'> Invoice </span>

<span id = 'number'> #{9}</span>
                              

</section>

", invoceObj?.OrderDate, invoceObj?.InvoiceDate, invoceObj?.InvoiceNo, invoceObj?.Userdetail.Name, invoceObj?.Userdetail.City, invoceObj?.Userdetail.Zipcode,
invoceObj?.Userdetail.State, invoceObj?.Userdetail.PhoneNo, invoceObj?.Userdetail.Email, invoceObj?.InvoiceNo);

            sb.Append(@"
<div class= 'clearfix'></div>


<section id = 'items'>


<table cellpadding = '0' cellspacing = '0'>


<tr>

<th></th> <!--Dummy cell for the row number and row commands-->

<th> Product Name </th>

<th> Quantity </th>

<th> Price </th>

<th> Discount </th>

<th> Tax </th>

<th> Total </th>

</tr>
");
            int i = 0;
            foreach (var item in invoceObj.Products)
            {
                sb.AppendFormat(@"<tr>
<td>{0} </td> <!--Don't remove this column as it's needed for the row commands -->
                                  <td>{1}</td>

                                  <td>{2} </td>
     
                                  <td>&#163;{3}</td>
          
                                  <td>&#163;{4}</td>
               
                                  <td> &#163;0 </td>
                    
                                  <td>&#163;{5}</td>
                                  </tr>
", i +=1, item.Name, item.Quantity, item.Price, item.Discount, item.Price * item.Quantity);
            }
            sb.AppendFormat(@"
                          
                                                          
                          

                                                        </table>
                          

                                                      </section>
                          

                                                      <div class= 'currency'>
                           
                                                         <span> *All prices are in </span>  <span> GBP</span>

                                                      </div>
                            
                            <section id='sums'>
                            
                              <table cellpadding='0' cellspacing='0'>

                                <tr>
                                  <th>Subtotal:</th>
                                  <td>&#163;{0} </td>
     
                                </tr>
     

                                     <tr data - iterate='tax'>
      
                                        <th> Shipping Cost:</th>
         
                                           <td>&#163;{1}</td>
              
                                       </tr>
              

                                              <tr class='amount-total'>
               
                                                     <th>Total:</th>
                  
                                                     <td>&#163;{2} </td>
                       
                                             </tr>
                       
                                
                              </table>
                              
                            </section>
                       </div>
   
                   </div>
   
               </div>
   
          

                            </body>
</html>
", invoceObj?.Subtotal, invoceObj?.ShippingCharge, invoceObj?.Subtotal + invoceObj?.ShippingCharge);

            return sb.ToString();
        }
    }
}


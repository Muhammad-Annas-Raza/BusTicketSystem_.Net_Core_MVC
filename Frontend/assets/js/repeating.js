List = [
    {
        a:"myModal",
        b:"Express",
        c:"bi bi-coin",
        d:"carouselExampleIndicators",
        e:"express",
        f:
        `<li><i class="clrr ri-check-double-line"></i>&nbsp; Travel with a great deal of comfort and quality.</li>
        <li><i class="clrr ri-check-double-line"></i>&nbsp; Having seperate capsule / square shaped compartment.</li>
        <li><i class="clrr ri-check-double-line"></i>&nbsp; With extra ordinary comfortable A/C section.</li>
        <li><i class="clrr ri-check-double-line"></i>&nbsp; With breakfast,lunch and dinner.</li>
        <li><i class="clrr ri-check-double-line"></i>&nbsp; With smart screen , wifi and headphones.</li>`

    },
    {
        a:"myModal1",
        b:"Luxury",
        c:"bi bi-cash",
        d:"carouselExampleIndicators1",
        e:"luxury",
        f:` <li><i class="clrr ri-check-double-line"></i>&nbsp; Travel with a great deal of comfort and quality.</li>
        <li><i class="clrr ri-check-double-line"></i>&nbsp; Having adjudtable seats.</li>
        <li><i class="clrr ri-check-double-line"></i>&nbsp; With extra ordinary comfortable A/C.</li>             
        <li><i class="clrr ri-check-double-line"></i>&nbsp; With smart screen , wifi and headphones.</li>     `

    },
    {
        a:"myModal2",
        b:"Volvo (A/C)",
        c:"bi bi-fan",
        d:"carouselExampleIndicators2",
        e:"AC",
        f: ` <li><i class="clrr ri-check-double-line"></i>&nbsp; Travel with comfort and quality.</li>        
        <li><i class="clrr ri-check-double-line"></i>&nbsp; Having Air Conditioner and comfortable seats.</li>    `

    },
    {
        a:"myModal3",
        b:"Volvo (Non A/C)",
        c:"bi bi-bag-check-fill",
        d:"carouselExampleIndicators3",
        e:"NonAC",
        f:` <li><i class="clrr ri-check-double-line"></i>&nbsp; Travel with comfort and quality.</li>        
        <li><i class="clrr ri-check-double-line"></i>&nbsp; Having comfortable seats.</li>`

    }
]

 
 


document.getElementById('put_modal_here').innerHTML = "";

var data = "";

for (let index = 0; index < List.length; index++) {
    data +=
    `<div id="${List[index].a}" class="modal">  
       <div class="modal-dialog  modal-dialog-centered">       
       <div class="modal-content">       
           <div class="modal-header">
               <h2 class="modal-title">${List[index].b} <i class="clrr icon ${List[index].c}"></i></h2>
               <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
           </div>
           <div class="modal-body">
             <div id="${List[index].d}" class="carousel slide" data-bs-ride="carousel">
               <div class="carousel-indicators">
                 <button type="button" data-bs-target="#${List[index].d}" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                 <button type="button" data-bs-target="#${List[index].d}" data-bs-slide-to="1" aria-label="Slide 2"></button>
                 <button type="button" data-bs-target="#${List[index].d}" data-bs-slide-to="2" aria-label="Slide 3"></button>
               </div>
               <div class="carousel-inner">
                 <div class="carousel-item active">
                   <img src="assets/img/Modal/${List[index].e}-1.jpg" class="d-block w-100" alt="Internet problem">
                 </div>
                 <div class="carousel-item">
                   <img src="assets/img/Modal/${List[index].e}-2.jpg" class="d-block w-100" alt="Internet problem">
                 </div>
                 <div class="carousel-item">
                   <img src="assets/img/Modal/${List[index].e}-3.jpg" class="d-block w-100" alt="Internet problem">
                 </div>
               </div>
               <button class="carousel-control-prev" type="button" data-bs-target="#${List[index].d}" data-bs-slide="prev">
                 <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                 <span class="visually-hidden">Previous</span>
               </button>
               <button class="carousel-control-next" type="button" data-bs-target="#${List[index].d}" data-bs-slide="next">
                 <span class="carousel-control-next-icon" aria-hidden="true"></span>
                 <span class="visually-hidden">Next</span>
               </button>
             </div>
 <br>
 
             <ul style="list-style: none;margin-left:-25px ;">
              ${List[index].f}
             </ul>   
           </div>
           <div class="modal-footer">
 
           <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
           
           </div>
           
       </div>
   
   </div>
 
 </div>`
 let a = (document.getElementById("put_modal_here").innerHTML = data);
    
}
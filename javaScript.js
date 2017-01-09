function hello() {
	console.log("hello")
};
/**/
function callbackHell(){
	function thoAnCo(callback){
	    setTimeout(function () {
			console.log("tho an co");	
			callback();			
		},2000);
	}
	
	var thoVaoHang =function (){
	    console.log("tho vao hang");
	}

	thoAnCo(thoVaoHang);
};

function es6() {
    
}

function keThua() {
    Number.prototype.read4Text = function(){
        var arrText = ["khong", "mot", "hai", "ba", "bon", "nam", "sau", "bay", "tam", "chin"];
        return arrText[this.valueOf()];
    }
	if(!String.prototype.trim){
		String.prototype.trim = function(){
			return this.valueOf();
		}
	}
	
	if(!Number.prototype.round){
		Number.prototype.round = function(length){
			var value=this.valueOf();
			if(length){
				var e=Math.pow(10,length)
				var tmp=value*e;
				tmp=(tmp-tmp%1)/e;
				return tmp;
			}				
			else
				return value-value%1;
		}
	}
	
	console.log(x.round());	
}

function arrow() {
     arr = [4, 5, 6, 7, 8];
    var arrOdd=arr.filter(v=>v%2==0);
	

    console.log(arrOdd);
}

//apply call
function tmp(){
	//khai bao
	var computer={
		accessWeb:function(site){
			console.log("go to "+site);
		}
	};
	//luu lai khai bao cu
	var oldFunc=computer.accessWeb;
	//khai bao moi
	computer.accessWeb=function(){
		console.log("start");
		oldFunc.apply(this,arguments);
		console.log("end");
	};
	computer.accessWeb("thiendia.com");
}

//bind(), apply() và call(). Cả 3 hàm này đều có cùng ý nghĩa là gán gián trị con trỏ “this” một cách tường minh.
 //Khác biệt của bind() là nó có giá trị trả về là “hàm gán sẵn giá trị cho this”, 
 //hàm apply() và call() đều thực hiện truyền giá trị cho “this” và kích hoạt hàm mục tiêu chạy, 
 //điểm khác biệt của apply() là nó nhận giá trị truyền vào là 1 mảng thay vì cần truyền rời rạc như hàm call().

 function tmp1(){
	function multi(a,b){
		return a*b;
	} 
	var multiBy2=multi.bind(this,2); 
	console.log(multiBy2(4));
 }

//sort arr with item is object
function sortArr(){
	//khai bao ham sort
	var arrayModul=(function(){
		var sort=function(array, fn){
			for(var i=0; i<array.length-1;i++)
				for(var j=i+1;j<array.length;j++)
					if(fn(array[i],array[j])>0){
						var tmp=array[i];
						array[i]=array[j];
						array[j]=tmp;
					}
		}
		return{
			sort
		}
	})();

	arr=[
		{age:10},{age:15},{age:5}
	]

	arrayModul.sort(arr,function(a,b){
		return a.age-b.age;
	})

	console.table(arr);
}















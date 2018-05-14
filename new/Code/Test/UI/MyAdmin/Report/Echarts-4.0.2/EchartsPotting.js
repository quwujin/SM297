
// 基于准备好的dom，初始化echarts实例
var myChart = "";

var RequestData = function (ChartsType) {

    myChart = echarts.init(document.getElementById(ChartsType), 'vintage');

    myChart.showLoading();

    $.post("/MyAdmin/Handler/ReportHandler.ashx", { GetResult: ChartsType }, function (result) {

        myChart.hideLoading();

        var resultData = $.parseJSON(result);

        //console.log(resultData.ObjectValue);

        if (resultData.Success == false) {
            resultData.ObjectValue = null;
        }

        //动态调用函数
        doCallback(ChartsType, resultData.ObjectValue);
    });

     
}

//这个方法做了一些操作、然后调用回调函数    
function doCallback(name, Data) {
    var Callback = eval(name);
    new Callback(Data);
}

///计算两个整数的百分比值 
function GetPercent(num, total) {
    num = parseFloat(num);
    total = parseFloat(total);
    if (isNaN(num) || isNaN(total)) {
        return 0;
    }
    return total <= 0 ? 0 : (Math.round(num / total * 10000) / 100.00);
}

//参与统计
var WheaterStatistics = function (Data) {
     
    var YesterdayData = [];
    var TodayData = [];
    var AllData = [];

    if (Data != null) {
        YesterdayData = Data.Data1;
        TodayData = Data.Data2;
        AllData = Data.Data3;
    }
      
    option = {
        title: [
            {
                text: '今日数据',
                x: '49%',
                y: '1%',
                textAlign: 'center',
             }
            ,
            {
                text: '昨日数据',
                x: '29%',
                y: '40%',
                textAlign: 'center',
             },
            {
                text: '累计数据',
                x: '70%',
                y: '40%',
                textAlign: 'center'
            }
        ],
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        }, 
        toolbox: {
            show: true,
            feature: {
                saveAsImage: {}
            }
        }, 
        series: [
            {
                name: '参与人次',
                type: 'pie',
                data: TodayData,
                label: {
                    formatter: '{b}: {c}人',
                    position: 'inside'
                },
                radius: 130,
                center: ['50%', '30%'],
            },
            {
                name: '中奖人数',
                type: 'pie',
                label: {
                    formatter: '{b}: {c}人',
                    position: 'inside'
                },
                radius: 130,
                center: ['30%', '70%'],
                data: YesterdayData

            },
            {
                name: '发奖人数',
                type: 'pie',
                label: {
                    formatter: '{b}: {c}人',
                    position: 'inside'
                },
                radius: 130,
                center: ['70%', '70%'],
                data: AllData
            }
        ]
    };

    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
}

//参与趋势与中奖趋势
var PartakeTrend = function (Data) {
      
    var timePoint = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23'];
    var partakePerson = [];
    var drawPerson = [];
     
    if (Data != null) {
         
        partakePerson = $.parseJSON(Data.Data1);
        drawPerson = $.parseJSON(Data.Data2);

        //console.log(partakePerson);
        //console.log(drawPerson);
    }
    
     
    option = {
        title: {
            text: '参与趋势与中奖趋势'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'cross',
                crossStyle: {
                    color: '#999'
                }
            }
        },
        toolbox: {
            feature: {
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        legend: {
            data: ['参与人次', '中奖人次']
        },
        xAxis: [
            {
                type: 'category',
                data: timePoint,
                axisPointer: {
                    type: 'shadow'
                },
                axisLabel: {
                    formatter: '{value} 点'
                }
            }
        ],
        yAxis: [
            {
                type: 'value',
                name: '人次',
                //min: 0,
                //max: 250,
                //interval: 50,
                axisLabel: {
                    formatter: '{value} 人'
                }
            } 
        ],
        series: [
            {
                name: '参与人次',
                type: 'line',
                data: partakePerson
            },
            {
                name: '中奖人次',
                type: 'bar',
                data: drawPerson
            }

        ]
    };
     

    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);

}

//全国参与分布图
var WholeCountry = function (Data) {
     
    var CountryData = [];
    //{name: '海门', value: 9},
    // { name: '鄂尔多斯', value: 12 }, 

    if (Data != null) {
        CountryData = $.parseJSON(Data.Data1);
        //console.log(data);
    }

    var geoCoordMap = {
        '海门': [121.15, 31.89],
        '鄂尔多斯': [109.781327, 39.608266],
        '招远': [120.38, 37.35],
        '舟山': [122.207216, 29.985295],
        '齐齐哈尔': [123.97, 47.33],
        '盐城': [120.13, 33.38],
        '赤峰': [118.87, 42.28],
        '青岛': [120.33, 36.07],
        '乳山': [121.52, 36.89],
        '金昌': [102.188043, 38.520089],
        '泉州': [118.58, 24.93],
        '莱西': [120.53, 36.86],
        '日照': [119.46, 35.42],
        '胶南': [119.97, 35.88],
        '南通': [121.05, 32.08],
        '拉萨': [91.11, 29.97],
        '云浮': [112.02, 22.93],
        '梅州': [116.1, 24.55],
        '文登': [122.05, 37.2],
        '上海': [121.48, 31.22],
        '攀枝花': [101.718637, 26.582347],
        '威海': [122.1, 37.5],
        '承德': [117.93, 40.97],
        '厦门': [118.1, 24.46],
        '汕尾': [115.375279, 22.786211],
        '潮州': [116.63, 23.68],
        '丹东': [124.37, 40.13],
        '太仓': [121.1, 31.45],
        '曲靖': [103.79, 25.51],
        '烟台': [121.39, 37.52],
        '福州': [119.3, 26.08],
        '瓦房店': [121.979603, 39.627114],
        '即墨': [120.45, 36.38],
        '抚顺': [123.97, 41.97],
        '玉溪': [102.52, 24.35],
        '张家口': [114.87, 40.82],
        '阳泉': [113.57, 37.85],
        '莱州': [119.942327, 37.177017],
        '湖州': [120.1, 30.86],
        '汕头': [116.69, 23.39],
        '昆山': [120.95, 31.39],
        '宁波': [121.56, 29.86],
        '湛江': [110.359377, 21.270708],
        '揭阳': [116.35, 23.55],
        '荣成': [122.41, 37.16],
        '连云港': [119.16, 34.59],
        '葫芦岛': [120.836932, 40.711052],
        '常熟': [120.74, 31.64],
        '东莞': [113.75, 23.04],
        '河源': [114.68, 23.73],
        '淮安': [119.15, 33.5],
        '泰州': [119.9, 32.49],
        '南宁': [108.33, 22.84],
        '营口': [122.18, 40.65],
        '惠州': [114.4, 23.09],
        '江阴': [120.26, 31.91],
        '蓬莱': [120.75, 37.8],
        '韶关': [113.62, 24.84],
        '嘉峪关': [98.289152, 39.77313],
        '广州': [113.23, 23.16],
        '延安': [109.47, 36.6],
        '太原': [112.53, 37.87],
        '清远': [113.01, 23.7],
        '中山': [113.38, 22.52],
        '昆明': [102.73, 25.04],
        '寿光': [118.73, 36.86],
        '盘锦': [122.070714, 41.119997],
        '长治': [113.08, 36.18],
        '深圳': [114.07, 22.62],
        '珠海': [113.52, 22.3],
        '宿迁': [118.3, 33.96],
        '咸阳': [108.72, 34.36],
        '铜川': [109.11, 35.09],
        '平度': [119.97, 36.77],
        '佛山': [113.11, 23.05],
        '海口': [110.35, 20.02],
        '江门': [113.06, 22.61],
        '章丘': [117.53, 36.72],
        '肇庆': [112.44, 23.05],
        '大连': [121.62, 38.92],
        '临汾': [111.5, 36.08],
        '吴江': [120.63, 31.16],
        '石嘴山': [106.39, 39.04],
        '沈阳': [123.38, 41.8],
        '苏州': [120.62, 31.32],
        '茂名': [110.88, 21.68],
        '嘉兴': [120.76, 30.77],
        '长春': [125.35, 43.88],
        '胶州': [120.03336, 36.264622],
        '银川': [106.27, 38.47],
        '张家港': [120.555821, 31.875428],
        '三门峡': [111.19, 34.76],
        '锦州': [121.15, 41.13],
        '南昌': [115.89, 28.68],
        '柳州': [109.4, 24.33],
        '三亚': [109.511909, 18.252847],
        '自贡': [104.778442, 29.33903],
        '吉林': [126.57, 43.87],
        '阳江': [111.95, 21.85],
        '泸州': [105.39, 28.91],
        '西宁': [101.74, 36.56],
        '宜宾': [104.56, 29.77],
        '呼和浩特': [111.65, 40.82],
        '成都': [104.06, 30.67],
        '大同': [113.3, 40.12],
        '镇江': [119.44, 32.2],
        '桂林': [110.28, 25.29],
        '张家界': [110.479191, 29.117096],
        '宜兴': [119.82, 31.36],
        '北海': [109.12, 21.49],
        '西安': [108.95, 34.27],
        '金坛': [119.56, 31.74],
        '东营': [118.49, 37.46],
        '牡丹江': [129.58, 44.6],
        '遵义': [106.9, 27.7],
        '绍兴': [120.58, 30.01],
        '扬州': [119.42, 32.39],
        '常州': [119.95, 31.79],
        '潍坊': [119.1, 36.62],
        '重庆': [106.54, 29.59],
        '台州': [121.420757, 28.656386],
        '南京': [118.78, 32.04],
        '滨州': [118.03, 37.36],
        '贵阳': [106.71, 26.57],
        '无锡': [120.29, 31.59],
        '本溪': [123.73, 41.3],
        '克拉玛依': [84.77, 45.59],
        '渭南': [109.5, 34.52],
        '马鞍山': [118.48, 31.56],
        '宝鸡': [107.15, 34.38],
        '焦作': [113.21, 35.24],
        '句容': [119.16, 31.95],
        '北京': [116.46, 39.92],
        '徐州': [117.2, 34.26],
        '衡水': [115.72, 37.72],
        '包头': [110, 40.58],
        '绵阳': [104.73, 31.48],
        '乌鲁木齐': [87.68, 43.77],
        '枣庄': [117.57, 34.86],
        '杭州': [120.19, 30.26],
        '淄博': [118.05, 36.78],
        '鞍山': [122.85, 41.12],
        '溧阳': [119.48, 31.43],
        '库尔勒': [86.06, 41.68],
        '安阳': [114.35, 36.1],
        '开封': [114.35, 34.79],
        '济南': [117, 36.65],
        '德阳': [104.37, 31.13],
        '温州': [120.65, 28.01],
        '九江': [115.97, 29.71],
        '邯郸': [114.47, 36.6],
        '临安': [119.72, 30.23],
        '兰州': [103.73, 36.03],
        '沧州': [116.83, 38.33],
        '临沂': [118.35, 35.05],
        '南充': [106.110698, 30.837793],
        '天津': [117.2, 39.13],
        '富阳': [119.95, 30.07],
        '泰安': [117.13, 36.18],
        '诸暨': [120.23, 29.71],
        '郑州': [113.65, 34.76],
        '哈尔滨': [126.63, 45.75],
        '聊城': [115.97, 36.45],
        '芜湖': [118.38, 31.33],
        '唐山': [118.02, 39.63],
        '平顶山': [113.29, 33.75],
        '邢台': [114.48, 37.05],
        '德州': [116.29, 37.45],
        '济宁': [116.59, 35.38],
        '荆州': [112.239741, 30.335165],
        '宜昌': [111.3, 30.7],
        '义乌': [120.06, 29.32],
        '丽水': [119.92, 28.45],
        '洛阳': [112.44, 34.7],
        '秦皇岛': [119.57, 39.95],
        '株洲': [113.16, 27.83],
        '石家庄': [114.48, 38.03],
        '莱芜': [117.67, 36.19],
        '常德': [111.69, 29.05],
        '保定': [115.48, 38.85],
        '湘潭': [112.91, 27.87],
        '金华': [119.64, 29.12],
        '岳阳': [113.09, 29.37],
        '长沙': [113, 28.21],
        '衢州': [118.88, 28.97],
        '廊坊': [116.7, 39.53],
        '菏泽': [115.480656, 35.23375],
        '合肥': [117.27, 31.86],
        '武汉': [114.31, 30.52],
        '大庆': [125.03, 46.58]
    };

    var convertData = function (data) {
        var res = [];
        for (var i = 0; i < data.length; i++) {
            var geoCoord = geoCoordMap[data[i].name];
            if (geoCoord) {
                res.push({
                    name: data[i].name,
                    value: geoCoord.concat(data[i].value)
                });
            }
        }
        console.log(res);
        return res;
    };

    option = {
        backgroundColor: '#404a59',
        title: {
            text: '全国主要城市参与分布图',
            //subtext: 'data from PM25.in',
            left: 'center',
            textStyle: {
                color: '#fff'
            }
        },
        tooltip: {
            trigger: 'item'
        },
        legend: {
            orient: 'vertical',
            y: 'bottom',
            x: 'right',
            data: ['显示所有名称'],
            textStyle: {
                color: '#fff'
            }
        },
        geo: {
            map: 'china',
            label: {
                emphasis: {
                    show: false
                }
            },
            roam: true,
            itemStyle: {
                normal: {
                    areaColor: '#323c48',
                    borderColor: '#111'
                },
                emphasis: {
                    areaColor: '#2a333d'
                }
            }
        },
        series: [
            {
                name: '显示所有名称',
                type: 'scatter',
                coordinateSystem: 'geo',
                data: convertData(CountryData),
                symbolSize: function (val) {
                    return val[2] / 10;
                },
                label: {
                    normal: {
                        formatter: '{b}',
                        position: 'right',
                        show: false
                    },
                    emphasis: {
                        show: true
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#ddb926'
                    }
                }
            },
            {
                name: 'Top 5',
                type: 'effectScatter',
                coordinateSystem: 'geo',
                data: convertData(CountryData.sort(function (a, b) {
                    //返回a-b升序，返回b-a降序
                    return b.value - a.value;
                }).slice(0, 6)),
                //symbolSize: function (val) {
                //    return val[2] / 10;
                //},
                showEffectOn: 'render',
                rippleEffect: {
                    brushType: 'stroke'
                },
                hoverAnimation: true,
                label: {
                    normal: {
                        formatter: '{b}',
                        position: 'right',
                        show: true
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#f4e925',
                        shadowBlur: 10,
                        shadowColor: '#333'
                    }
                },
                zlevel: 1
            }
        ]
    };


    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);

}

//奖项统计
var AwardStatistics = function (Data) {
     
    var drawPerson = [];//中奖人数
    var awardDrawPerson = [];//发奖人数
    var awardRate = [];//发放率

    if (Data != null) {
        drawPerson = Data.Data1;
        awardDrawPerson = $.parseJSON(Data.Data2);

        drawPerson.forEach(function (ele, index) {
            if (awardDrawPerson[index].value>0)
                awardRate[index] = { name: drawPerson[index].name, value: GetPercent(awardDrawPerson[index].value, drawPerson[index].value) };
        });

        //console.log(drawPerson);
        //console.log(awardDrawPerson);
        //console.log(awardRate);
    }

    option = {
        title: [
            {
                text: '中\n奖\n人\n数',
                x: '0%',
                y: '15%',
                textAlign: 'center',
                left: 40
            }
            ,
            {
                text: '发\n奖\n人\n数',
                x: '0%',
                y: '65%',
                textAlign: 'center',
                left: 40
            }, 
            {
                text: '发放率', 
                x: '85%',
                y: '15%',
                textAlign: 'center'
            }
        ],
        tooltip: {
            trigger: 'axis'
        }, 
        grid: [//坐标轴位置控制
            {
                top: 50,
                width: '50%',
                height: '40%',
                bottom: '45%',
                left: 80,
                containLabel: true
            },
            {
                top: '55%',
                width: '50%',
                bottom: 10,
                left: 70,
                containLabel: true
            }
        ],
        toolbox: {
            show: true,
            feature: {
                dataZoom: {
                    yAxisIndex: 'none'
                },
                dataView: { readOnly: false },
                magicType: { type: ['line', 'bar'] },
                restore: {},
                saveAsImage: {}
            }
        },
        xAxis: [
            {
                type: 'category',
                splitLine: {
                    show: false
                },
                data: ['一等奖', '二等奖', '三等奖', '四等奖', '五等奖', '六等奖']
            },
            {
                type: 'category',
                gridIndex: 1,
                splitLine: {
                    show: false
                },
                data: ['一等奖', '二等奖', '三等奖', '四等奖', '五等奖', '六等奖']
            }
        ],
        yAxis: [
            {
                type: 'value',
                axisLabel: {
                    interval: 0,
                    rotate: 30
                },
                splitLine: {
                    show: false
                },
                axisLabel: {
                    formatter: '{value} 人'
                }
            },
            {
                type: 'value',
                gridIndex: 1,
                axisLabel: {
                    interval: 0,
                    rotate: 30
                },
                splitLine: {
                    show: false
                },
                axisLabel: {
                    formatter: '{value} 人'
                }
            }
        ],
        series: [
            {
                name: '中奖人数',
                type: 'line',
                data: drawPerson,
                stack: 'name',
                silent: true,
                itemStyle: {
                    normal: {
                        color: 'red'
                    }
                },
                markPoint: {
                    data: [
                        { type: 'max', name: '最大值' },
                        { type: 'min', name: '最小值' }
                    ]
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                }
            },
            {
                name: '发奖人数',
                type: 'line',
                data: awardDrawPerson,
                xAxisIndex: 1,
                yAxisIndex: 1,
                markPoint: {
                    data: [
                        { type: 'max', name: '最大值' },
                        { type: 'min', name: '最小值' }
                    ]
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                }
            },
            {
                name: '发放率',
                type: 'pie',
                radius: [0, '45%'],
                center: ['85%', '45%'],
                data: awardRate,
                label: {
                    formatter: '{b}: {c}%',
                    position: 'inside'
                },
                itemStyle: { opacity: 1 }
            }

        ]
    };


    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);

}
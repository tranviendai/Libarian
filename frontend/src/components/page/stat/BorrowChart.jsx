import React from 'react';
import {
    Chart as ChartJS,
    LinearScale,
    CategoryScale,
    BarElement,
    PointElement,
    LineElement,
    Legend,
    Tooltip,
    LineController,
    BarController,
} from 'chart.js';
import { Chart } from 'react-chartjs-2';

ChartJS.register(
    LinearScale,
    CategoryScale,
    BarElement,
    PointElement,
    LineElement,
    Legend,
    Tooltip,
    LineController,
    BarController
);

const BorrowChart = ({ months }) => { 
    const labels = months.map(x => "Tháng " + x.month);

    const data = {
        labels,
        datasets: [
            {
                type: 'line',
                label: 'Tổng lượt mượn',
                borderColor: 'rgb(255, 99, 132)',
                borderWidth: 2,
                fill: false,
                data: months.map(x => x.sum),
            },
            {
                type: 'bar',
                label: 'Số lượt mượn',
                backgroundColor: 'rgb(75, 192, 192)',
                data: months.map(x => x.borrow),
                borderColor: 'white',
                borderWidth: 2,
            },
            {
                type: 'bar',
                label: 'Số lượt trả',
                backgroundColor: 'rgb(53, 162, 235)',
                data: months.map(x => x.ret)
            },
        ],
    };

    const options = {
        plugins: {
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Chart.js Bar Chart',
            },
        },
    };

    return <Chart type='bar' data={data} options={options}/>
}

export default BorrowChart
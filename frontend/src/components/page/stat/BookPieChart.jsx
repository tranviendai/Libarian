import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js'
import { Pie } from 'react-chartjs-2';

ChartJS.register(ArcElement, Tooltip, Legend);

const BookPieChart = ({ BooksData }) => { 

    let sumCount = BooksData ? BooksData.countDamaged + BooksData.countLost + BooksData.countBorrowed + BooksData.countAvailable : 0;
    const getPercentage = (x) => ` (${Math.round(x / sumCount * 10000)/ 100}%)`;

    const PieData = {
        labels: [`Sách hỏng` + getPercentage(BooksData?.countDamaged),
        'Sách mất' + getPercentage(BooksData?.countLost),
        'Đang mượn' + getPercentage(BooksData?.countBorrowed),
        'Sách còn' + getPercentage(BooksData?.countAvailable)],
        datasets: [
            {
                label: 'Số lượng',
                data: [BooksData?.countDamaged, BooksData?.countLost, BooksData?.countBorrowed, BooksData?.countAvailable],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(153, 102, 255, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(153, 102, 255, 1)'
                ],
                borderWidth: 1,
            },
        ],
    };

    return <Pie data={PieData} />
}

export default BookPieChart;
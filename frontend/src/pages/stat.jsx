import { useMemo, useState } from "react";
import DataTable from "../components/shared/data-table";
import useFetch from "../utils/useFetch";
import BookPieChart from "../components/page/stat/BookPieChart";
import BorrowChart from "../components/page/stat/BorrowChart";

const StatPage = () => { 

    const [startDate, setStartDate] = useState('2020-01-01')
    const [endDate, setEndDate] = useState('2022-12-31')

    const DateObj = useMemo(() => { 
        return {
            params: {
                start: startDate,
                end: endDate
            }
        }
    }, [startDate, endDate])
    //books
    const { data: BooksData } = useFetch('/statistics/data/books', DateObj);
    const headersBooksHigh = ['Mã sách', 'Tên sách', 'Số lượt mượn'];
    const rowsBooksHigh = BooksData?.booksHigh?.map(x => {
        const y = {};
        y.onRowSelected = () => {}
        y.rowData = ['#' + x.book.bookID, x.book.title, x.count]

        return y;
    });

    const headersBooksLow = ['Mã sách', 'Tên sách', 'Số lượt mượn'];
    const rowsBooksLow = BooksData?.booksLow?.map(x => {
        const y = {};
        y.onRowSelected = () => { }
        y.rowData = ['#' + x.book.bookID, x.book.title, x.count]

        return y;
    });

    const headersLBooks = ['Mã đầu sách', 'Tên sách', 'Ghi chú'];
    const rowsLBooks = BooksData?.lbookDamaged.map(x => {
        const y = {};
        y.onRowSelected = () => { }
        y.rowData = ['#' + x.lBookID, x.title, x.note]

        return y;
    });

    //borrows
    const { data: BorrowsData } = useFetch('/statistics/data/borrow', DateObj);

    //fines
    const { data: FineData } = useFetch('/statistics/data/fine');

    return <div className="container-80">
        <div className="page-title mb-5">Thống kê</div>

        <div className="mt-3 mb-5 row">
            <div className="col-3">
                <h3>Ngày thống kê: </h3>
            </div>
            <div className="col-3">
                <div className="input-group">
                    <span className="input-group-text" id="basic-addon1">Từ ngày</span>
                    <input type="date" className="form-control" value={startDate} onChange={(e)=> setStartDate(e.target.value)} />
                </div>
            </div>
            <div className="col-3">
                <div className="input-group">
                    <span className="input-group-text" id="basic-addon1">Đến ngày</span>
                    <input type="date" className="form-control" value={endDate} onChange={(e) => setEndDate(e.target.value)} />
                </div>
            </div>
        </div>
        <h2>Thống kê sách: </h2>
        <div className="row mb-5">
            <div className="col-4 p-3">
                <p><b className="h4">{BooksData?.countBook}</b> sách</p>
                <p><b className="h4">{BooksData?.countLbook}</b> đầu sách</p>
                <p><b className="h4">{BooksData?.countBorrowed}</b> sách đang cho mượn</p>
                <p><b className="h4">{BooksData?.countDamaged}</b> sách bị hỏng</p>
                <p><b className="h4">{BooksData?.countLost}</b> sách bị mất</p>
                <p><b className="h4">{BooksData?.countAvailable}</b> sách còn lại</p>
            </div>
            <div className="col-4">
                <BookPieChart BooksData={BooksData} />
            </div>
        </div>

        <div className="row mb-3">
            <div className="col-5">
                <h3>Sách được mượn nhiều: </h3>
                <DataTable rows={rowsBooksHigh} headers={headersBooksHigh} noEdit />
            </div>
            <div className="col-1"></div>
            <div className="col-5">
                <h3>Sách được mượn ít: </h3>
                <DataTable rows={rowsBooksLow} headers={headersBooksLow} noEdit />
            </div>
        </div>

        <h3>Sách hỏng chưa thanh lý: </h3>
        <div className="mb-5">
            <DataTable rows={rowsLBooks} headers={headersLBooks} noEdit />
        </div>

        <h3>Thống kê mượn sách: </h3>
        <div className="pt-3">
            <p><b className="h4">{BorrowsData?.countBorrow}</b> lượt mượn</p>
            <p><b className="h4">{BorrowsData?.countReturn}</b> trả sách nguyên vẹn</p>
        </div>

        <div className="mt-3 mb-5">
            {BorrowsData && <BorrowChart months={BorrowsData.months} />}
        </div>

        <h3>Thống kê thẻ thư viện & phiếu phạt: </h3>
        <p><b className="h4">{FineData?.countCard}</b> thẻ thư viện</p>
        <p><b className="h4">{FineData?.countBan}</b> đang bị khóa</p>
        <p><b className="h4">{FineData?.countFine}</b> lượt vi phạm</p>
        <p><b className="h4">Tổng tiền phạt: {FineData?.totalFine.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}đ</b></p>
    </div>
}

export default StatPage;
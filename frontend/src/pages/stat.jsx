import DataTable from "../components/shared/data-table";
import useFetch from "../utils/useFetch";

const StatPage = () => { 
    const { data: BooksData } = useFetch('/statistics/data/books');

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

    return <div className="container-80">
        <div className="page-title">Thống kê</div>

        <h3>Thống kê sách: </h3>
        <p><b>{BooksData?.countBook}</b> sách</p>
        <p><b>{BooksData?.countLbook}</b> đầu sách</p>
        <p><b>{BooksData?.countBorrowed}</b> sách đang cho mượn</p>
        <p><b>{BooksData?.countDamaged}</b> sách bị hỏng</p>
        <p><b>{BooksData?.countLost}</b> sách bị mất</p>
        <p><b>{BooksData?.countAvailable}</b> sách còn lại</p>

        <h3>Sách được mượn nhiều: </h3>
        <DataTable rows={rowsBooksHigh} headers={headersBooksHigh} noEdit />

        <h3>Sách được mượn ít: </h3>
        <DataTable rows={rowsBooksLow} headers={headersBooksLow} noEdit />
    </div>
}

export default StatPage;
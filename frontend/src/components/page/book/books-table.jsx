import { useNavigate } from "react-router-dom";
import { getBookCover } from "../../../mock-data";
import DataTable from "../../shared/data-table";

const BooksTable = ({ bookList }) => { 
    
    const navigate = useNavigate();
    const headers = ['', 'Mã', 'Tên', 'Tác giả', 'Năm XB', 'NXB', 'Ngày thêm'];
    const rows = bookList.map(x => { 
        const y = {};
        y.onRowSelected = () => navigate(`/BLibrary/Book/${x.bookID}`);
        y.rowData = [<img src={getBookCover(x.bookID)} alt="" style={{ width: '100px' }} />,
            '#' + x.bookID, x.title, x.author, x.publishingYear, x.publisher, x.addDate]

        return y;
    })

    return <DataTable headers={headers} rows={rows} />
}

export default BooksTable;
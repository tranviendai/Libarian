import { Link } from "react-router-dom";
import BG from '../../resources/imgs/book-bg.jpg'
import Side from '../../resources/imgs/book-side.jpg'
import BookDetailDialog from "./dialog-book-detail";
import { useState } from "react";
import useGlobalContext from "../../contexts/GlobalContext";

const BooksGrid = ({ bookList, nCol }) => { 

    const [selectedBook, setSelectedBook] = useState(null);
    const { token } = useGlobalContext();

    let style;
    if (nCol) style = { gridTemplateColumns: `repeat(${nCol}, 1fr)`}

    const onSelectBook = (e, x) => { 
        if (token) return;
        e.preventDefault();

        setSelectedBook(x);
    }

    return <div className="books-grid" style={style} > 
        {bookList.map(x => (
            <Link to={`/LMS/Book/${x.bookID}`} key={x.bookID} onClick={(event) => onSelectBook(event, x)}>
                <div className="book-card">
                    <div className="img-wrap" style={{'--book-bg': `url(${BG})`, '--book-side': `url(${Side})`}}>
                        <div className="img"  alt="" style={{'--bg': `url(${x.image})`}}/>
                    </div>
                    <div className="title" title={x.title}>{x.title}</div>
                    <div className="author" title={x.author}>{x.author}</div>
                </div>
            </Link>
        ))}

        {selectedBook && <BookDetailDialog onExit={() => setSelectedBook(null)} book={selectedBook} />}
    </div>
}

export default BooksGrid;
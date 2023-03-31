import { Link } from "react-router-dom";

const BooksGrid = ({ bookList, nCol }) => { 
    let style;
    if (nCol) style = { gridTemplateColumns: `repeat(${nCol}, 1fr)`}

    return <div className="books-grid" style={style} > 
        {bookList.map(x => (
            <Link to={`/LMS/Book/${x.bookID}`} key={x.bookID}>
                <div className="book-card">
                    <div className="img-wrap">
                        <img src={x.image} alt="" />
                    </div>
                    <div className="title" title={x.title}>{x.title}</div>
                    <div className="author" title={x.author}>{x.author}</div>
                </div>
            </Link>
        ))}
    </div>
}

export default BooksGrid;
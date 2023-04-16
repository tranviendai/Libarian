import { Link } from "react-router-dom";
import BG from '../../resources/imgs/book-bg.jpg'
import Side from '../../resources/imgs/book-side.jpg'

const BooksGrid = ({ bookList, nCol }) => { 
    let style;
    if (nCol) style = { gridTemplateColumns: `repeat(${nCol}, 1fr)`}

    return <div className="books-grid" style={style} > 
        {bookList.map(x => (
            <Link to={`/LMS/Book/${x.bookID}`} key={x.bookID}>
                <div className="book-card">
                    <div className="img-wrap" style={{'--book-bg': `url(${BG})`, '--book-side': `url(${Side})`}}>
                        <div className="img"  alt="" style={{'--bg': `url(${x.image})`}}/>
                    </div>
                    <div className="title" title={x.title}>{x.title}</div>
                    <div className="author" title={x.author}>{x.author}</div>
                </div>
            </Link>
        ))}
    </div>
}

export default BooksGrid;
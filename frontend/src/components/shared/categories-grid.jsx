
const CategoriesGrid = ({ nCol, list, onSelect }) => { 

    return <div className="categories-grid" style={{ '--nCol': nCol || 6 }}>
        {list.map(x =>
            <div className="category btn" key={x.categoryID} onClick={()=> onSelect(x)}>
                <div className="name">{x.nameCategory}</div>
                <div className="number">{x.count}</div>
            </div>
        )}
    </div>
}

export default CategoriesGrid;
import React, { useState } from "react";
import './Folder.css';
import { FaFolder, FaFile } from 'react-icons/fa';

const Folder = ({ folder }) => {
  const [isOpen, setIsOpen] = useState(false);

  const toggleOpen = () => {
    setIsOpen(!isOpen);
  };

  const getFolderContent = (folder) => {
    if (!isOpen) {
      return null;
    }
    return (
      <div className="folder-tree">
      <div className="node">
        <ul>
        {folder.type === 'folder' && <FaFolder className="icon" />}
          {folder.content.map((item, index) => (
            <li key={index}>
              {item.type === "file" ? (
                <span>{item.name}</span>
              ) : (
                <Folder folder={item} />
              )}
            </li>
          ))}
        </ul>
      </div>
      </div>
    );
  };

  return (
    <>
      <div onClick={toggleOpen}>{folder.name}</div>
      {getFolderContent(folder)}
    </>
  );
};

export default Folder;
